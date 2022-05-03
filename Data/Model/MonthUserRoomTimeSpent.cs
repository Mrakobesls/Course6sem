using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Model
{
    [Table(nameof(MonthUserRoomTimeSpent))]
    public class MonthUserRoomTimeSpent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int RoomId { get; set; }
        public TimeSpan TotalTime { get; set; }

        //[ForeignKey("UserId")]
        public virtual User User { get; set; }
        //[ForeignKey("RoomId")]
        public virtual Room Room { get; set; }
    }
}