namespace UI.Models.Contract
{
    public class CreateContractModel
    {
        public string Subject { get; set; }

        public string Address { get; set; }

        public decimal Price { get; set; }

        public int ClientId { get; set; }
    }
}
