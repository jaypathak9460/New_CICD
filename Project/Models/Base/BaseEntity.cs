using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace angular_crud.Models.Base
{
    public abstract class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public Guid Id { get; set; }

        public bool IsDelete { get; set; }
        public BaseEntity()
        {
            Id = new Guid();
            IsDelete = false;
          
        }
    }
}
