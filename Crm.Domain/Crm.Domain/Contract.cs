using System;

namespace Crm.Domain
{
    public class Contract
    {
        public int Id { get; }

        public string Subject { get; }

        public string Address { get; }

        public decimal Price { get; }

        public int ClientId { get; }

        public Client Client { get; }

        public ICollection<Employee> Employees { get; }

        public ICollection<WorkPlan> WorkPlans { get; }

        public Contract(
            string subject, 
            string address, 
            decimal price, 
            int clientId)
        {
            if(string.IsNullOrEmpty(subject))
            {
                throw new ArgumentException(nameof(subject));
            }

            if(string.IsNullOrEmpty(address))
            {
                throw new ArgumentException(nameof(address));
            }

            Subject = subject;
            Address = address;
            Price = price;
            ClientId = clientId;

            Employees = new List<Employee>();

            WorkPlans = new List<WorkPlan>();
        }

        private Contract()
        { }

        public void AddEmployee(Employee employee)
        {
            Employees.Add(employee);
        }

        public void AddWorkPlan(WorkPlan workPlan)
        {
            WorkPlans.Add(workPlan);
        }
    }
}
