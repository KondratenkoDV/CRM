namespace UI.Models.WorkPlan
{
    public class WorkPlan
    {
        public int Id { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateFinish { get; set; }

        public int ContractId { get; set; }
    }
}
