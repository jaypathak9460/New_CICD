namespace anuglar_crud.Models.Dto
{
    public class RoleDTO
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; }
        public bool IsDelete { get; set; }
       
        public List<Guid>? PermissionList { get; set; }
    }
}
