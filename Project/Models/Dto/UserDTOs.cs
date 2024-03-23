using angular_crud.Models.Domain;

namespace angular_crud.Models.Dto
{
    public class UserDTOs
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;  
        public string LastName { get; set; } = string.Empty;  
        public DateTime? DateOfBirth { get; set; } 

        public List<Guid>? RoleList { get; set; } 

    }   public class ListUserDTOs
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;  
        public string LastName { get; set; } = string.Empty;  
        public DateTime? DateOfBirth { get; set; } 
        public string Roles { get; set; } = string.Empty;
        public string Permissions { get; set; } = string.Empty;

    }

}
