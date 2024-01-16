using BE.Models;
using System.ComponentModel.DataAnnotations;

namespace BE.Areas.Admin.ViewModels
{
    public class CreateEmployeeVM
    {
        [Required(ErrorMessage ="Is required")]
        [Range(1, 25, ErrorMessage = "Wrong lenght 1-25")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Is required")]
        [Range(1, 25, ErrorMessage = "Wrong lenght 1-25")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Is required")]
        public IFormFile Photo { get; set; }

        [Required(ErrorMessage = "Is required")]
        public int PositionId { get; set; }
        public ICollection<Position>? Positions { get; set; }

        public string? FaceLink { get; set; }
        public string? TwitLink { get; set; }
        public string? GoogleLink { get; set; }
    }
}
