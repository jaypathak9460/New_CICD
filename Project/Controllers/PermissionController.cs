using angular_crud.Data;
using anuglar_crud.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace anuglar_crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase

    {
        private static AngularDBContext _dbContext;

        public PermissionController(AngularDBContext dbContext) {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<List<Permissions>> GetAllPermissionsAsync() {
            var permissionList = await _dbContext.Permissions.ToListAsync();
            return permissionList;
        
        }
    }
}
