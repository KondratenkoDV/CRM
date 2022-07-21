using System;
using Xunit;
using Domain.Enum;

namespace Domain.Tests
{
    public class ClientTests
    {
        [Fact]
        public void When_CreatingNewClient_Expect_Successfully()
        {
            // Arrange

            var name = "name";

            var codeOfTheCountry = CodeOfTheCountry.Ukraine;

            var regionCode = "00";

            var subscriberNumbersubs = "0000000";

            // Act

            var client = new Client(
                name,
                codeOfTheCountry,
                regionCode,
                subscriberNumbersubs);

            // Assert

            Assert.Equal(name, client.Name);
            Assert.Equal(codeOfTheCountry, client.СodeOfTheCountry);
            Assert.Equal(regionCode, client.RegionCode);
            Assert.Equal(subscriberNumbersubs, client.SubscriberNumber);
        }

        [Fact]
        public void When_AddContract_Expect_ContractWasAddedToCollection()
        {
            // Arrenge

            var subject = "subject";

            var address = "address";

            var price = 0;

            var clientId = 0;

            var contract = new Contract(
                subject,
                address,
                price,
                clientId);

            var name = "name";

            var codeOfTheCountry = CodeOfTheCountry.Ukraine;

            var regionCode = "00";

            var subscriberNumbersubs = "0000000";

            var client = new Client(
                name,
                codeOfTheCountry,
                regionCode,
                subscriberNumbersubs);

            // Act

            client.AddContract(contract);

            // Assert

            Assert.NotEmpty(client.Contracts);
        }

        [Fact]
        public void When_ChangeName_Expect_ChangeNameWasAddedToClient()
        {
            // Arrenge

            var name = "name";

            var codeOfTheCountry = CodeOfTheCountry.Ukraine;

            var regionCode = "00";

            var subscriberNumbersubs = "0000000";

            var client = new Client(
                name,
                codeOfTheCountry,
                regionCode,
                subscriberNumbersubs);

            var newName = "newName";

            // Act

            client.ChangeName(newName);

            // Assert

            Assert.Equal(newName, client.Name);
        }

        [Fact]
        public void When_ChangePhoneNumber_Expect_ChangePhoneNumberWasAddedToClient()
        {
            // Arrenge

            var name = "name";

            var codeOfTheCountry = CodeOfTheCountry.Ukraine;

            var regionCode = "00";

            var subscriberNumbersubs = "0000000";

            var client = new Client(
                name,
                codeOfTheCountry,
                regionCode,
                subscriberNumbersubs);

            var newCodeOfTheCountry = CodeOfTheCountry.Ukraine;

            var newRegionCode = "11";

            var newSubscriberNumbersubs = "1111111";

            // Act

            client.ChangePhoneNumber(
                newCodeOfTheCountry, 
                newRegionCode, 
                newSubscriberNumbersubs);

            // Assert

            Assert.Equal(newRegionCode, client.RegionCode);
            Assert.Equal(newSubscriberNumbersubs, client.SubscriberNumber);
        }
    }
}
