using angular_crud.Models.Base;
using angular_crud.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace anuglar_crud.Models.Domain
{
    public class UserRoles : BaseEntity
    {
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public Users? User { get; set; } = null;

        public Guid RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Roles? Role { get; set; } = null;
    }
}
