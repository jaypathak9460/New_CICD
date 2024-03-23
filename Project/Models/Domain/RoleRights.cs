using angular_crud.Models.Base;
using angular_crud.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace anuglar_crud.Models.Domain
{
    public class RoleRights: BaseEntity
    {
        public Guid RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Roles Roles { get; set; }


        public Guid PermissionId { get; set; }
        [ForeignKey("PermissionId")]
        public Permissions Permissions { get; set; }

    }
}
