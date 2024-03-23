using angular_crud.Data;
using angular_crud.Models.Domain;
using anuglar_crud.Models.Domain;
using anuglar_crud.Models.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace anuglar_crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private static AngularDBContext _dbContext;
        private static IMapper _mapper;
        public RolesController(AngularDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        // GET: api/<RolesController>
        [HttpGet]
        public async Task<List<Roles>> GetAsync()
        {
            var roleslist = await _dbContext.Roles.Where(s=>!s.IsDelete).ToListAsync();
            return roleslist;

        }

        // GET api/<RolesController>/5
        [HttpGet("{id}")]
        public async Task<RoleDTO> GetAsync(Guid id)
        {
            try
            {
                var role = await _dbContext.Roles.FindAsync(id);
                if (role == null)
                {
                    throw new Exception("No data Found");
                }
                else
                {

                    var roleDTO = _mapper.Map<RoleDTO>(role);
                    roleDTO.PermissionList = await _dbContext.RoleRights.Where(s => s.RoleId == roleDTO.Id).Select(s => s.PermissionId).ToListAsync();

                    return roleDTO;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // POST api/<RolesController>
        [HttpPost]
        public async Task<RoleDTO> PostAsync([FromBody] RoleDTO value)
        {
            try
            {
                var role = _mapper.Map<Roles>(value);
                role.Id = Guid.NewGuid();
                _dbContext.Roles.Add(role);
                await _dbContext.SaveChangesAsync();


                List<RoleRights> permissionListToAdd = new List<RoleRights>();

                if (value.PermissionList != null && value.PermissionList.Count() > 0) {
                    foreach (var permission in value.PermissionList) {
                        RoleRights permissions = new RoleRights
                        {
                            RoleId = role.Id,
                            PermissionId = permission
                        };
                        permissionListToAdd.Add(permissions);
                    }
                }

                if (permissionListToAdd.Count() > 0) { 
                   await  _dbContext.RoleRights.AddRangeAsync(permissionListToAdd);
                }
                await _dbContext.SaveChangesAsync();
                value.Id = role.Id;
                return value;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT api/<RolesController>/5
        [HttpPut("{id}")]
        public async Task<RoleDTO> PutAsync(Guid id, [FromBody] RoleDTO value)

        {
            try
            {
                if  (id == Guid.Empty)
                {
                  return await PostAsync(value);

                }
                else
                {
                    Roles? role = await _dbContext.Roles.FindAsync(value.Id);
                    if (role != null)
                    {
                        role.RoleName = value.RoleName;
                        //update the existing entries
                        

                        var ExistingPermissions = _dbContext.RoleRights.Where(s => s.RoleId == role.Id).ToList();
                        _dbContext.RoleRights.RemoveRange(ExistingPermissions);


                        List<RoleRights> permissionListToAdd = new List<RoleRights>();

                        if (value.PermissionList != null && value.PermissionList.Count() > 0)
                        {
                            foreach (var permission in value.PermissionList)
                            {
                                RoleRights permissions = new RoleRights
                                {
                                    RoleId = role.Id,
                                    PermissionId = permission
                                };
                                permissionListToAdd.Add(permissions);
                            }
                        }

                        if (permissionListToAdd.Count() > 0)
                        {
                            await _dbContext.RoleRights.AddRangeAsync(permissionListToAdd);
                        }
                        await _dbContext.SaveChangesAsync();
                        return value;
                    }
                    else
                    {
                        throw new Exception("No Role Matches the Value");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE api/<RolesController>/5
        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id)
        {
          var role=  await _dbContext
            .Roles.FindAsync(id);
            if (role != null)
            {
                role.IsDelete = true;
                await _dbContext.SaveChangesAsync();
            }
            else {
                throw new Exception("No Roles found");
            }


        }
    }
}
