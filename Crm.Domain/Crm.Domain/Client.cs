

namespace Crm.Domain
{
    public class Client : IName, IClient
    {
        private string? _name;

        private string? _phonNumber;

        public int Id { get; }

        public string? Name { get => _name; }

        public string? PhonNumber { get => _phonNumber; }

        public ICollection<Contract> Contracts { get; }

        public Client(string name)
        {
            _name = name;

            Contracts = new List<Contract>();
        }

        public void AddPhonNumber(int codeOfTheCountry, int regionCode, int subscriberNumber)
        {
            _phonNumber = $"+{codeOfTheCountry} ({regionCode}) {subscriberNumber}";
        }

        public void AddContract(Contract contract)
        {
            Contracts?.Add(contract);
        }
    }
}
