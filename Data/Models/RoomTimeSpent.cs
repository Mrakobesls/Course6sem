using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    [Table(nameof(RoomTimeSpent))]
    public class RoomTimeSpent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int RoomId { get; set; }
        [Required]
        public int EnterPassageDateId { get; set; }
        [Required]
        public int ExitPassageDateId { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }
        public virtual PassageDate EnterPassageDate { get; set; }
        public virtual PassageDate ExitPassageDate { get; set; }
    }
}