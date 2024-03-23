using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace anuglar_crud.Services
{
    public class ADONetServices
    {

        private readonly string _connectionString;
        public IConfiguration _configuration;
        public ADONetServices(IConfiguration config)
        {

            _configuration = config;
             _connectionString = _configuration.GetConnectionString("DefaultConnection");

        }
        //copied from chatGpt 
        public async Task<IEnumerable<T>> CallStoredProcedureAsync<T>(string storedProcedureName) where T : new()
        {
            var result = new List<T>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                        var columnNames = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();

                        while (await reader.ReadAsync())
                        {
                            var item = new T();

                            foreach (var property in properties)
                            {
                                var columnName = property.Name;

                                if (columnNames.Contains(columnName))
                                {
                                    var value = reader[columnName];
                                    if (value != DBNull.Value)
                                    {
                                        property.SetValue(item, value, null);
                                    }
                                }
                            }

                            result.Add(item);
                        }
                    }
                }
            }

            return result;
        }


    }
}
