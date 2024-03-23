using angular_crud.Models.Base;
using anuglar_crud.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace angular_crud.Models.Domain
{
    public class Roles: BaseEntity
    {
        public string? RoleName { get; set; }
        [ForeignKey("Id")]

        public UserRoles? UserRoles { get; set; }   

    }
}
