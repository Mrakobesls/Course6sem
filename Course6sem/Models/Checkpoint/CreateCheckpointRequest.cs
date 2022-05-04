using System.ComponentModel.DataAnnotations;

namespace Application.Models.Checkpoint
{
    public class CreateCheckpointRequest
    {
        [Required(ErrorMessage = "Не указано название")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Не указано описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Не указана комната")]
        public int FirstRoomId { get; set; }

        [Required(ErrorMessage = "Не указана комната")]
        public int SecondRoomId { get; set; }
    }
}
