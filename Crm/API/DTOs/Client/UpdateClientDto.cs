using Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Client
{
    public class UpdateClientDto
    {
        [Required(ErrorMessage = "Enter new name")]
        [MinLength(2, ErrorMessage = "Minimum length 2 characters")]
        [MaxLength(50, ErrorMessage = "Maximum length 50 characters")]
        public string NewName { get; set; }

        public CodeOfTheCountry NewСodeOfTheCountry { get; set; }

        [Required(ErrorMessage = "Enter new region code")]
        [MinLength(2, ErrorMessage = "Minimum length 2 characters")]
        [MaxLength(2, ErrorMessage = "Maximum length 2 characters")]
        public string NewRegionCode { get; set; }

        [Required(ErrorMessage = "Enter new subscriber number")]
        [MinLength(7, ErrorMessage = "Minimum length 7 characters")]
        [MaxLength(7, ErrorMessage = "Maximum length 7 characters")]
        public string NewSubscriberNumber { get; set; }
    }
}
