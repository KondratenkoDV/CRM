using System;

namespace Crm.Domain
{
    public class Employee
    {
        public int Id { get; }

        public string Name { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public int PositionId { get; private set; }

        public Position Position { get; }

        public ICollection<Contract> Contracts { get; }

        public Employee(
            string firstName, 
            string lastName, 
            int positionId)
        {
            if(string.IsNullOrEmpty(firstName))
            {
                throw new ArgumentException(nameof(firstName));
            }

            if(string.IsNullOrEmpty(lastName))
            {
                throw new ArgumentException(nameof(lastName));
            }

            FirstName = firstName;
            LastName = lastName;
            PositionId = positionId;

            AddName();

            Contracts = new List<Contract>();
        }

        private Employee()
        { }

        public void AddContract(Contract contract)
        {
            Contracts.Add(contract);
        }

        public void AddName()
        {
            Name = $"{LastName} {FirstName}";
        }

        public void ChangeName(string lastName, string firstName)
        {
            if (lastName != null && firstName != null)
            {
                LastName = lastName;

                FirstName = firstName;

                AddName();
            }
        }

        public void ChangePositionId(int positionId)
        {
            PositionId = positionId;
        }
    }
}
