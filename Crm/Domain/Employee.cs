using System;

namespace Domain
{
    public class Employee
    {
        public int Id { get; }

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
            if (string.IsNullOrEmpty(firstName))
            {
                throw new ArgumentException(nameof(firstName));
            }

            if (string.IsNullOrEmpty(lastName))
            {
                throw new ArgumentException(nameof(lastName));
            }

            FirstName = firstName;
            LastName = lastName;
            PositionId = positionId;

            Contracts = new List<Contract>();
        }

        private Employee()
        { }

        public void AddContract(Contract contract)
        {
            Contracts.Add(contract);
        }

        public void ChangeFirstName(string firstName)
        {
            if (firstName != null)
            {                
                FirstName = firstName;
            }
        }

        public void ChangeLastName(string lastName)
        {
            if (lastName != null)
            {
                LastName = lastName;                
            }
        }

        public void ChangePositionId(int positionId)
        {
            PositionId = positionId;
        }
    }
}
