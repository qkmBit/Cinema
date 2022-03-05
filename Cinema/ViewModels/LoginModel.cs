using System.ComponentModel.DataAnnotations;

namespace Cinema.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Не указан номер телефона")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}