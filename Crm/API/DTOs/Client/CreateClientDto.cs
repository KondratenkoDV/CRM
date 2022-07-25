using Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Client
{
    public class CreateClientDto
    {
        [Required(ErrorMessage = "Enter name")]
        [MinLength(2, ErrorMessage = "Minimum length 2 characters")]
        [MaxLength(50, ErrorMessage = "Maximum length 50 characters")]
        public string Name { get; set; }

        public CodeOfTheCountry СodeOfTheCountry { get; set; }

        [Required(ErrorMessage = "Enter region code")]
        [MinLength(2, ErrorMessage = "Minimum length 2 characters")]
        [MaxLength(2, ErrorMessage = "Maximum length 2 characters")]
        public string RegionCode { get; set; }

        [Required(ErrorMessage = "Enter subscriber number")]
        [MinLength(7, ErrorMessage = "Minimum length 7 characters")]
        [MaxLength(7, ErrorMessage = "Maximum length 7 characters")]
        public string SubscriberNumber { get; set; }
    }
}
