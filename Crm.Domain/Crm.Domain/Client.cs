using System;

namespace Crm.Domain
{
    public class Client
    {
        public int Id { get; }

        public string Name { get; }

        public string PhonNumber { get; }

        public ICollection<Contract> Contracts { get; }

        public Client(
            string name, 
            CodeOfTheCountry codeOfTheCountry, 
            string regionCode, 
            string subscriberNumber)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(nameof(name));
            }

            if(string.IsNullOrEmpty(regionCode) || !int.TryParse(regionCode, out int code) || regionCode.Length != 2)
            {
                throw new ArgumentException(nameof(regionCode));
            }

            if (string.IsNullOrEmpty(regionCode) || !int.TryParse(subscriberNumber, out int number) || subscriberNumber.Length != 7)
            {
                throw new ArgumentException(nameof(subscriberNumber));
            }

            Name = name;
            PhonNumber = $"+{(int)codeOfTheCountry} ({regionCode}) {subscriberNumber}";

            Contracts = new List<Contract>();
        }

        private Client()
        { }

        public void AddContract(Contract contract)
        {
            Contracts.Add(contract);
        }
    }
}
