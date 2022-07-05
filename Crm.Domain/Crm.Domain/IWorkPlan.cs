

namespace Crm.Domain
{
    public  interface IWorkPlan
    {
        DateTime DateStart { get; }

        DateTime DateFinish { get; }

        int ContractId { get; }

        Contract? Contract { get; }
    }
}
