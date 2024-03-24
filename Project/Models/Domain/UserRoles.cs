using angular_crud.Models.Base;
using angular_crud.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace anuglar_crud.Models.Domain
{
    public class UserRoles : BaseEntity
    {
        public Guid UserId { get; set; }
        

        public Guid RoleId { get; set; }
        
    }
}
