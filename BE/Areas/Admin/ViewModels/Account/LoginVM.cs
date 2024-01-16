using System.ComponentModel.DataAnnotations;

namespace BE.Areas.Admin.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Is required")]
        [Range(1,255,ErrorMessage ="Wrong lenght 1-255")]
        public string UserNameOrEmail { get; set; }

        [Required(ErrorMessage = "Is required")]
        [DataType(DataType.Password)]
        [Range(1, 35, ErrorMessage = "Wrong lenght 1-35")]
        public string Password { get; set; }
    }
}
