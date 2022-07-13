using System;

namespace Crm.Domain
{
    public class Client
    {
        public int Id { get; }

        public string Name { get; private set;  }

        public string PhoneNumber { get; private set; }

        public CodeOfTheCountry СodeOfTheCountry { get; private set; }

        public string RegionCode { get; private set; }

        public string SubscriberNumber { get; private set; }

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

            if (string.IsNullOrEmpty(subscriberNumber) || !int.TryParse(subscriberNumber, out int number) || subscriberNumber.Length != 7)
            {
                throw new ArgumentException(nameof(subscriberNumber));
            }

            Name = name;
            СodeOfTheCountry = codeOfTheCountry;
            RegionCode = regionCode;
            SubscriberNumber = subscriberNumber;

            AddPhoneNumber();

            Contracts = new List<Contract>();
        }

        private Client()
        { }

        public void AddContract(Contract contract)
        {
            Contracts.Add(contract);
        }

        public void AddPhoneNumber()
        {
            PhoneNumber = $"+{(int)СodeOfTheCountry} ({RegionCode}) {SubscriberNumber}";
        }

        public void ChangeName(string name)
        {
            if (name != null)
            {
                Name = name;
            }
        }

        public void ChangePhoneNumber(CodeOfTheCountry codeOfTheCountry, string regionCode, string subscriberNumber)
        {
            if (regionCode != null && subscriberNumber != null)
            {
                if (int.TryParse(regionCode, out int code) && regionCode.Length == 2)
                {
                    RegionCode = regionCode;
                }

                if (int.TryParse(subscriberNumber, out int number) && subscriberNumber.Length == 7)
                {
                    SubscriberNumber = subscriberNumber;
                }

                СodeOfTheCountry = codeOfTheCountry;

                AddPhoneNumber();
            }
        }
    }
}
