using System.ComponentModel.DataAnnotations;

namespace Cinema.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "�� ������ ����� ��������")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "�� ������ ������")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "������ ������ �������")]
        public string ConfirmPassword { get; set; }
    }
}