using System;

namespace Crm.Domain
{
    public class Position
    {
        public int Id { get; }

        public string Name { get; }

        public ICollection<Employee> Employees { get; }

        public Position(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(nameof(name));
            }

            Name = name;

            Employees = new List<Employee>();
        }

        public void AddEmployee(Employee employee)
        {
            Employees.Add(employee);
        }
    }
}
