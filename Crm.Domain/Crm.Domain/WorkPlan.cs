using System;

namespace Crm.Domain
{
    public class WorkPlan
    {
        public int Id { get; }

        public DateTime DateStart { get; private set; }

        public DateTime DateFinish { get; private set; }

        public int ContractId { get; private set; }

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

        private WorkPlan()
        { }

        public void ChangeDateStart(DateTime dateStart)
        {
            DateStart = dateStart;
        }

        public void ChangeDateFinish(DateTime dateFinish)
        {
            DateFinish = dateFinish;
        }

        public void ChangeContractId(int contractId)
        {
            ContractId = contractId;
        }
    }
}
