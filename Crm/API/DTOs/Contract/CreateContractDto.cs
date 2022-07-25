using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Contract
{
    public class CreateContractDto
    {
        [Required(ErrorMessage = "Enter subject")]
        [MinLength(2, ErrorMessage = "Minimum length 2 characters")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Enter address")]
        [MinLength(2, ErrorMessage = "Minimum length 2 characters")]
        public string Address { get; set; }

        public decimal Price { get; set; }

        public int ClientId { get; set; }
    }
}
