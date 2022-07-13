using System;

namespace Crm.Domain
{
    public class Contract
    {
        public int Id { get; }

        public string Subject { get; private set; }

        public string Address { get; private set; }

        public decimal Price { get; private set; }

        public int ClientId { get; private set; }

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

        public void ChangeSubject(string subject)
        {
            if(subject != null)
            {
                Subject = subject;
            }
        }

        public void ChangeAddress(string address)
        {
            if (address != null)
            {
                Address = address;
            }
        }

        public void ChangePrice(decimal price)
        {
            Price = price;        
        }

        public void ChangeClientId(int clientId)
        {
            ClientId = clientId;
        }
    }
}
