using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Model
{
    [Table(nameof(User))]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Login { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }
        [Required]
        [Column(TypeName = "varchar(35)")]
        public string Password { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(30)")]
        public string Surname { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public string Patronymic { get; set; }

        [Required]
        public int RoleId { get; set; }
        [Required]
        public int CurrentRoomId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
        [ForeignKey("CurrentRoomId")]
        public virtual Room CurrentRoom { get; set; }

        public virtual ICollection<AccessLevel> AccessLevels { get; set; }
        public virtual ICollection<MonthUserRoomTimeSpent> MonthRoomTimeSpents { get; set; }
    }
}
