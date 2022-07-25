using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Employee
{
    public class CreateEmployeeDto
    {
        [Required(ErrorMessage = "Enter first name")]
        [MinLength(2, ErrorMessage = "Minimum length 2 characters")]
        [MaxLength(50, ErrorMessage = "Maximum length 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Enter last name")]
        [MinLength(2, ErrorMessage = "Minimum length 2 characters")]
        [MaxLength(50, ErrorMessage = "Maximum length 50 characters")]
        public string LastName { get; set; }

        public int PositionId { get; set; }
    }
}
