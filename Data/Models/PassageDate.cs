using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Data.Models
{
    [Table(nameof(PassageDate))]
    public class PassageDate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        [Required]
        public int CheckpointId { get; set; }
        [Required]
        public DateTime Date { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("CheckpointId")]
        public virtual Checkpoint Checkpoint { get; set; }

        [JsonIgnore]
        public virtual RoomTimeSpent StartTimeRoomSpent { get; set; }
        [JsonIgnore]
        public virtual RoomTimeSpent EndTimeRoomSpent { get; set; }
    }
}