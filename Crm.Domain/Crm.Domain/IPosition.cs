

namespace Crm.Domain
{
    public interface IPosition
    {
        ICollection<Employee>? Employees { get; }

        void AddEmployee(Employee employee);
    }
}
