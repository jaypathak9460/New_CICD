using angular_crud.Models.Base;
using anuglar_crud.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.ExceptionServices;

namespace angular_crud.Models.Domain
{
    public class Users : BaseEntity
    { 
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [ForeignKey("UserId")]
        public virtual UserRoles? UserRoles { get; set; }

    }
}
