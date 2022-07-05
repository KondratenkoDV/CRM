

namespace Crm.Domain
{
    public class Employee : IName, IEmployee
    {
        private string? _name;

        private int _positionId;

        public int Id { get; }

        public string? Name { get => _name; }

        public int PositionId { get => _positionId; }

        public Position? Position { get; }

        public ICollection<Contract> Contracts { get; }

        public Employee(int positionId)
        {
            _positionId = positionId;

            Contracts = new List<Contract>();
        }

        public void AddName(string name, string surname)
        {
            _name = $"{surname} {name}";
        }

        public void AddContract(Contract contract)
        {
            Contracts?.Add(contract);
        }
    }
}
