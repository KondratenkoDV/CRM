namespace API.DTOs.WorkPlan
{
    public class CreateWorkPlanDto
    {
        public DateTime DateStart { get; set; }

        public DateTime DateFinish { get; set; }

        public int ContractId { get; set; }
    }
}
