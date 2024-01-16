using System.ComponentModel.DataAnnotations;

namespace BE.Areas.Admin.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Is required")]
        [Range(1, 25, ErrorMessage = "Wrong lenght 1-25")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Is required")]
        [Range(1, 25, ErrorMessage = "Wrong lenght 1-25")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Is required")]
        [Range(1, 25, ErrorMessage = "Wrong lenght 1-25")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Is required")]
        [Range(1, 255, ErrorMessage = "Wrong lenght 1-255")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Is required")]
        [DataType(DataType.Password)]
        [Range(1, 35, ErrorMessage = "Wrong lenght 1-35")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Is required")]
        [DataType(DataType.Password)]
        [Range(1, 35, ErrorMessage = "Wrong lenght 1-35")]
        [Compare(nameof(Password), ErrorMessage ="Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
