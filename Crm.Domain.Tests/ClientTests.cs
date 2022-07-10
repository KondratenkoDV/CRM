using Crm.Domain;
using Xunit;

namespace Crm.Domain.Tests
{
    public class ClientTests
    {
        [Fact]
        public void When_CreatingNewClient_Expect_Successfully()
        {
            // Arrenge

            var name = "name";

            var codeOfTheCountry = CodeOfTheCountry.Ukraine;

            var regionCode = "00";

            var subscriberNumbersubs = "0000000";

            var expected = "+380 (00) 0000000";

            // Act

            var client = new Client(name, codeOfTheCountry, regionCode, subscriberNumbersubs);

            // Assert

            Assert.Equal(name, client.Name);
            Assert.Equal(expected, client.PhonNumber);
        }

        [Fact]
        public void When_AddContract_Expect_ContractWasAddedToCollection()
        {
            // Arrenge

            var subject = "subject";

            var address = "address";

            var price = 0;

            var clientId = 0;

            var contract = new Contract(subject, address, price, clientId);

            var name = "name";

            var codeOfTheCountry = CodeOfTheCountry.Ukraine;

            var regionCode = "00";

            var subscriberNumbersubs = "0000000";

            var client = new Client(name, codeOfTheCountry, regionCode, subscriberNumbersubs);

            // Act

            client.AddContract(contract);

            // Assert

            Assert.NotEmpty(client.Contracts);
        }
    }
}
