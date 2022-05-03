using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Model
{
    [Table(nameof(Role))]
    public class Role
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
