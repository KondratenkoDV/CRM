

namespace Crm.Domain
{
    public class WorkPlan : IWorkPlan
    {
        private DateTime _dateStart;

        private DateTime _dateFinish;

        private int _contractId;

        public int Id { get; }

        public DateTime DateStart { get => _dateStart; }

        public DateTime DateFinish { get => _dateFinish; }

        public int ContractId { get => _contractId; }

        public Contract? Contract { get; }

        public WorkPlan(
            DateTime dateStart, 
            DateTime dateFinish, 
            int contractId)
        {
            _dateStart = dateStart;
            _dateFinish = dateFinish;
            _contractId = contractId;
        }
    }
}
