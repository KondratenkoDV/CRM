﻿namespace API.DTOs.WorkPlan
{
    public class UpdateWorkPlanDto
    {
        public DateTime NewDateStart { get; set; }

        public DateTime NewDateFinish { get; set; }

        public int NewContractId { get; set; }
    }
}
