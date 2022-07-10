using System;

namespace Crm.Domain
{
    public class WorkPlan
    {
        public int Id { get; }

        public DateTime DateStart { get; }

        public DateTime DateFinish { get; }

        public int ContractId { get; }

        public Contract Contract { get; }

        public WorkPlan(
            DateTime dateStart, 
            DateTime dateFinish, 
            int contractId)
        {
            DateStart = dateStart;
            DateFinish = dateFinish;
            ContractId = contractId;
        }

        public WorkPlan()
        { }
    }
}
