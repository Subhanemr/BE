using System.ComponentModel.DataAnnotations;

namespace BE.Areas.Admin.ViewModels
{
    public class UpdateSettingsVM
    {
        [Required(ErrorMessage = "Is required")]
        [Range(1, 255, ErrorMessage = "Wrong lenght 1-255")]
        public string Key { get; set; }

        [Required(ErrorMessage = "Is required")]
        [Range(1, 255, ErrorMessage = "Wrong lenght 1-255")]
        public string Value { get; set; }
    }
}
