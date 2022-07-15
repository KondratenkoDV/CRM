using System;

namespace Crm.Application.Crud.WorkPlan
{
    public class WorkPlanParameters
    {
        public int Id { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateFinish { get; set; }

        public int ContractId { get; set; }
    }
}
