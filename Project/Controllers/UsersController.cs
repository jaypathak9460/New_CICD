using angular_crud.Data;
using angular_crud.Models.Domain;
using angular_crud.Models.Dto;
using anuglar_crud.Models.Domain;
using anuglar_crud.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace anuglar_crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static AngularDBContext _dbContext;
        private static ADONetServices _adoservices;
        private static IConfiguration _configuration;
        private readonly IMapper _mapper;
        public UsersController(AngularDBContext dbContext, IMapper mapper,IConfiguration configuration)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _configuration = configuration;
            _adoservices = new ADONetServices(configuration);

        }
        // GET: api/<UsersController>
        [HttpGet]
        public async Task<List<ListUserDTOs>> GetAsync()
        {
            try
            {
              
                var userList = await _adoservices.CallStoredProcedureAsync<ListUserDTOs>("sp_List_User");

                return userList.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<UserDTOs> GetbyIdAsync(Guid id)
        {
            try
            {
                var user = await _dbContext.Users.FindAsync(id);
                if (user != null)
                {
                    var userDTOs = _mapper.Map<UserDTOs>(user);
                    var roleList = _dbContext.UserRoles.Where(s => s.UserId == userDTOs.Id).Select(s => s.RoleId).ToList();
                    if (roleList != null)
                    {
                        userDTOs.RoleList = roleList;
                    }

                    return userDTOs;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<UserDTOs> PostAsync([FromBody] UserDTOs dto)
        {
            try
            {
                var value = _mapper.Map<Users>(dto);
                value.Id = Guid.NewGuid();
                _dbContext.Users.Add(value);
                await _dbContext.SaveChangesAsync();

                if (dto.RoleList != null && dto.RoleList.Count() > 0)
                {
                    List<UserRoles> addRolelist = new List<UserRoles>();
                    foreach (var role in dto.RoleList)
                    {
                        UserRoles userRoles = new UserRoles
                        {
                            UserId = value.Id,
                            RoleId = role,
                        };
                        addRolelist.Add(userRoles);
                    }
                    if (addRolelist.Count > 0)
                    {
                        _dbContext.AddRange(addRolelist);
                    }
                    await _dbContext.SaveChangesAsync();

                }
                dto.Id = value.Id;
                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<UserDTOs> PutAsync(Guid? id, [FromBody] UserDTOs DTO)
        {
            try
            {
                if (DTO.Id != null || DTO.Id != Guid.Empty)
                {

                    Users? user = await _dbContext.Users.FindAsync(DTO.Id);
                    if (user != null)
                    {
                        // update Old entries
                        user.FirstName = DTO.FirstName;
                        user.LastName = DTO.LastName;
                        user.DateOfBirth = DTO.DateOfBirth;


                        // remove old mapping
                        var oldUserRoleMapping = _dbContext.UserRoles.Where(s => s.UserId == user.Id).ToList();
                        _dbContext.UserRoles.RemoveRange(oldUserRoleMapping);
                        // add new role mapping from UI
                        if (DTO.RoleList != null && DTO.RoleList.Count > 0)
                        {
                            //create a list to add mapping 
                            List<UserRoles> addRolelist = new List<UserRoles>();
                            foreach (var role in DTO.RoleList)
                            {
                                UserRoles userRoles = new UserRoles
                                {
                                    UserId = user.Id,
                                    RoleId = role,
                                };
                                addRolelist.Add(userRoles);
                            }
                            if (addRolelist.Count > 0)
                            {
                                // add a List to daatabase
                                _dbContext.AddRange(addRolelist);
                            }
                        }

                        //update the existing entries
                        await _dbContext.SaveChangesAsync();
                        return DTO;
                    }
                    else
                    {
                        throw new Exception("No Role Matches the Value");
                    }
                }
                else
                {
                    return await PostAsync(DTO);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            var user = await _dbContext
           .Users.FindAsync(id);
            if (user != null)
            {
                user.IsDelete = true;
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("No User Found");
            }
        }
    }
}
