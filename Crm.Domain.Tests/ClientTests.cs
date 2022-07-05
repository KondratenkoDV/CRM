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

            // Act

            var client = new Client(name);

            // Assert

            Assert.Equal(name, client.Name);
        }

        [Fact]
        public void When_AddPhonNumber_Expect_PhonNumberWasAddedToClient()
        {
            // Arrange

            var codeOfTheCountry = CodeOfTheCountry.Ukraine;

            var regionCode = "00";

            var subscriberNumber = "0000000";

            var expected = $"+{(int)codeOfTheCountry} ({int.Parse(regionCode)}) {int.Parse(subscriberNumber)}";

            // Act

            var client = new Client(null!);

            client.AddPhonNumber((int)codeOfTheCountry, int.Parse(regionCode), int.Parse(subscriberNumber));

            // Assert

            Assert.Equal(expected, client.PhonNumber);
        }

        [Fact]
        public void When_AddContract_Expect_ContractWasAddedToCollection()
        {
            // Arrenge

            var subject = "test";

            var contract = new Contract(subject, null!, 0, 1);

            // Act

            var client = new Client(null!);

            client.AddContract(contract);

            // Assert

            Assert.NotEmpty(client.Contracts);
        }
    }
}
