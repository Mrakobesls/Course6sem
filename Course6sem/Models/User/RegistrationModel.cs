using System.ComponentModel.DataAnnotations;

namespace ShemTeh.App.Models.User
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "Не указан Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан Email")]
        [MaxLength(60)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан имя")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указана фамилия")]
        [MaxLength(50)]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Не указано отчество")]
        [MaxLength(50)]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
    }
}
