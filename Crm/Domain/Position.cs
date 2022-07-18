using System;

namespace Domain
{
    public class Position
    {
        public int Id { get; }

        public string Name { get; private set; }

        public ICollection<Employee> Employees { get; }

        public Position(string name)
        {
            if (string.IsNullOrEmpty(name))
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
        public void ChangeName(string name)
        {
            if (name != null)
            {
                Name = name;
            }
        }
    }
}
