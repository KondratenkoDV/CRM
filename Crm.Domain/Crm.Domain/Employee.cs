using System;

namespace Crm.Domain
{
    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; }

        public int PositionId { get; }

        public Position Position { get; }

        public ICollection<Contract> Contracts { get; }

        public Employee(
            string name, 
            string surname, 
            int positionId)
        {
            if(string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(nameof(name));
            }

            if(string.IsNullOrEmpty(surname))
            {
                throw new ArgumentException(nameof(surname));
            }

            Name = $"{surname} {name}";
            PositionId = positionId;

            Contracts = new List<Contract>();
        }

        public Employee()
        { }

        public void AddContract(Contract contract)
        {
            Contracts.Add(contract);
        }
    }
}
