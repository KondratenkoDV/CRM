

namespace Crm.Domain
{
    public interface IContract
    {
        string? Subject { get; }

        string? Address { get; }

        decimal Price { get; }

        int ClientId { get; }

        Client? Client { get; }

        ICollection<Employee>? Employees { get; }

        ICollection<WorkPlan>? WorkPlans { get; }

        void AddEmployee(Employee employee);

        void AddWorkPlan(WorkPlan workPlan);
    }
}
