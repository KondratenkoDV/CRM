using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Employee
{
    public class UpdateEmployeeDto
    {
        [Required(ErrorMessage = "Enter new first Name")]
        [MinLength(2, ErrorMessage = "Minimum length 2 characters")]
        [MaxLength(50, ErrorMessage = "Maximum length 50 characters")]
        public string NewFirstName { get; set; }

        [Required(ErrorMessage = "Enter new last Name")]
        [MinLength(2, ErrorMessage = "Minimum length 2 characters")]
        [MaxLength(50, ErrorMessage = "Maximum length 50 characters")]
        public string NewLastName { get; set; }

        public int NewPositionId { get; set; }
    }
}
