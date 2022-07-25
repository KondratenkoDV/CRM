using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Position
{
    public class CreatePositionDto
    {
        [Required(ErrorMessage = "Enter name")]
        [MinLength(2, ErrorMessage = "Minimum length 2 characters")]
        [MaxLength(50, ErrorMessage = "Maximum length 50 characters")]
        public string Name { get; set; }
    }
}
