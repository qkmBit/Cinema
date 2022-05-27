using System.ComponentModel.DataAnnotations;

namespace Cinema.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "�� ������ ����� ��������")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "�� ������ ������")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "���������?")]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}