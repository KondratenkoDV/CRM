

namespace Crm.Domain
{
    public class Contract : IContract
    {
        private string? _subject;

        private string? _address;

        private decimal _price;

        private int _clientId;

        public int Id { get; }

        public string? Subject { get => _subject; }

        public string? Address { get => _address; }

        public decimal Price { get => _price; }

        public int ClientId { get => _clientId; }

        public Client? Client { get; }

        public ICollection<Employee> Employees { get; }

        public ICollection<WorkPlan> WorkPlans { get; }

        public Contract(
            string subject, 
            string address, 
            decimal price, 
            int clientId)
        {
            _subject = subject;
            _address = address;
            _price = price;
            _clientId = clientId;

            Employees = new List<Employee>();

            WorkPlans = new List<WorkPlan>();
        }

        public void AddEmployee(Employee employee)
        {
            Employees?.Add(employee);
        }

        public void AddWorkPlan(WorkPlan workPlan)
        {
            WorkPlans?.Add(workPlan);
        }
    }
}
