using System.ComponentModel.DataAnnotations;

namespace BE.Areas.Admin.ViewModels
{
    public class CreatePositionVM
    {
        [Required(ErrorMessage = "Is required")]
        [Range(1, 25, ErrorMessage = "Wrong lenght 1-25")]
        public string Name { get; set; }

    }
}
