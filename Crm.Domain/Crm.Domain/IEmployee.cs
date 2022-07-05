

namespace Crm.Domain
{
    public interface IEmployee
    {
        int PositionId { get; }

        Position? Position { get; }

        ICollection<Contract>? Contracts { get; }

        void AddName(string name, string surname);

        void AddContract(Contract contract);
    }
}
