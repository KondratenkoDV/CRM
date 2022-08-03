using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Contract
{
    public class CreateContractDto
    {
        public string Subject { get; set; }

        public string Address { get; set; }

        public decimal Price { get; set; }

        public int ClientId { get; set; }
    }
}
