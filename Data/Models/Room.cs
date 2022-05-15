using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Data.Models
{
    [Table(nameof(Room))]
    public class Room
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

        public virtual ICollection<Checkpoint> Checkpoints { get; set; }
        [JsonIgnore]
        public virtual ICollection<RoomTimeSpent> RoomTimeSpents { get; set; }
        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; }
        [JsonIgnore]
        public virtual ICollection<MonthUserRoomTimeSpent> MonthUserRoomTimeSpents { get; set; }
    }
}