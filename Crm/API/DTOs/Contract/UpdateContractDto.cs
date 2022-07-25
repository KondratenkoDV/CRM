using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Contract
{
    public class UpdateContractDto
    {
        [Required(ErrorMessage = "Enter new subject")]
        [MinLength(2, ErrorMessage = "Minimum length 2 characters")]
        public string NewSubject { get; set; }

        [Required(ErrorMessage = "Enter address")]
        [MinLength(2, ErrorMessage = "Minimum length 2 characters")]
        public string NewAddress { get; set; }

        public decimal NewPrice { get; set; }

        public int NewClientId { get; set; }
    }
}
