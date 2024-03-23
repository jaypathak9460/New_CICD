using angular_crud.Models.Domain;
using anuglar_crud.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace angular_crud.Data
{
    public class AngularDBContext : DbContext
    {
        public AngularDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<RoleRights> RoleRights { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
