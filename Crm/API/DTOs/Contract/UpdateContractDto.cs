namespace API.DTOs.Contract
{
    public class UpdateContractDto
    {
        public string NewSubject { get; set; }

        public string NewAddress { get; set; }

        public decimal NewPrice { get; set; }

        public int NewClientId { get; set; }
    }
}
