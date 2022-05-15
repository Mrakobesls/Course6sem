using System.ComponentModel.DataAnnotations;

namespace Application.Models.User
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "Не указан Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан Email")]
        [EmailAddress(ErrorMessage = "Введен невалидный Email")]
        [MaxLength(60)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан имя")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указана роль")]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Не указана должность")]
        public int PositionId { get; set; }

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
        [Required(ErrorMessage = "Не введен пароль повторно")]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
        public List<int> AccessLevelsId { get; set; } = new List<int>();
    }
}
