using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Position
{
    public class UpdatePositionDto
    {
        [Required(ErrorMessage = "Enter new name")]
        [MinLength(2, ErrorMessage = "Minimum length 2 characters")]
        [MaxLength(50, ErrorMessage = "Maximum length 50 characters")]
        public string NewName { get; set; }
    }
}
