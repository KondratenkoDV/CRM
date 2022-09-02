namespace UI.Models.Contract
{
    public class UpdateContractModel
    {
        public string NewSubject { get; set; }

        public string NewAddress { get; set; }

        public decimal NewPrice { get; set; }

        public int NewClientId { get; set; }
    }
}
