using System.ComponentModel.DataAnnotations;

namespace Application.Models.User
{
    public class ChangePasswordRequest
    {
        [Required(ErrorMessage = "Введите старый пароль")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Введите новый пароль")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Введите новый пароль ещё раз")]
        [Compare("NewPassword", ErrorMessage = "Новый пароль не совпадает")]
        public string ConfirmNewPassword { get; set; }
    }
}
