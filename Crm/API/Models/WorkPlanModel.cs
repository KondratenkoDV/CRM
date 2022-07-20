namespace API.Models
{
    public class WorkPlanModel
    {
        public int Id { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateFinish { get; set; }

        public int ContractId { get; set; }
    }
}
