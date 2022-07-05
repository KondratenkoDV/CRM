

namespace Crm.Domain
{
    public class Position : IName, IPosition
    {
        private string? _name;

        public int Id { get; }

        public string? Name { get => _name; }

        public ICollection<Employee> Employees { get; }

        public Position(string name)
        {
            _name = name;

            Employees = new List<Employee>();
        }

        public void AddEmployee(Employee employee)
        {
            Employees?.Add(employee);
        }
    }
}
