

namespace Crm.Domain
{
    public interface IClient
    {
        string? PhonNumber { get; }

        ICollection<Contract>? Contracts { get; }

        void AddPhonNumber(int codeOfTheCountry, int regionCode, int subscriberNumber);
    }
}
