using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Data.Models
{
    [Table(nameof(Checkpoint))]
    public class Checkpoint
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Description { get; set; }

        public virtual ICollection<AccessLevel> AccessLevels { get; set; } = new List<AccessLevel>();
        [JsonIgnore]
        public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}