using System;
using Xunit;
using Application.Tests.Connection;
using Application.Services.Client;
using Domain.Enum;

namespace Application.Tests
{
    public class ClientServiceTests
    {
        [Fact]
        public async void Task_When_AddToDb_Expect_ClientWasAddedToDb()
        {
            // Arrange

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var clientService = new ClientService(context);

            var name = "name";

            var codeOfTheCountry = CodeOfTheCountry.Ukraine;

            var regionCode = "00";

            var subscriberNumber = "0000000";

            // Act

            await clientService.AddAsync(
                name,
                codeOfTheCountry,
                regionCode,
                subscriberNumber,
                cancellationToken);

            // Assert

            Assert.NotNull(context.Clients);
        }

        [Fact]
        public async void Task_When_SelectingAsync_Expect_ClientWasSelectFromDb()
        {
            // Arrenge

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var clientService = new ClientService(context);

            var name = "name";

            var codeOfTheCountry = CodeOfTheCountry.Ukraine;

            var regionCode = "00";

            var subscriberNumber = "0000000";

            int id = await clientService.AddAsync(
                name,
                codeOfTheCountry,
                regionCode,
                subscriberNumber,
                cancellationToken);

            // Act

            var client = await clientService.SelectingAsync(id);

            // Assert

            Assert.Equal(id, client.Id);
        }

        [Fact]
        public async void Task_When_UpdateAsync_Expect_ClientWasUpdateInDb()
        {
            // Arrenge

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var clientService = new ClientService(context);

            var name = "name";

            var codeOfTheCountry = CodeOfTheCountry.Ukraine;

            var regionCode = "00";

            var subscriberNumber = "0000000";

            int id = await clientService.AddAsync(
                name,
                codeOfTheCountry,
                regionCode,
                subscriberNumber,
                cancellationToken);

            var client = await clientService.SelectingAsync(id);

            var newName = "newName";

            var newRegionCode = "11";

            var newSubscriberNumber = "1111111";

            // Act

            await clientService.UpdateAsync(
                client,
                newName,
                codeOfTheCountry,
                newRegionCode,
                newSubscriberNumber,
                cancellationToken);

            // Assert

            Assert.Equal(newName, context.Clients.Single(c => c.Id == id).Name);
            Assert.Equal(newRegionCode, context.Clients.Single(c => c.Id == id).RegionCode);
            Assert.Equal(newSubscriberNumber, context.Clients.Single(c => c.Id == id).SubscriberNumber);
        }

        [Fact]
        public async void Task_When_DeleteAsync_Expect_ClientWasDeleteInDb()
        {
            // Arrenge

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var clientService = new ClientService(context);

            var name = "name";

            var codeOfTheCountry = CodeOfTheCountry.Ukraine;

            var regionCode = "00";

            var subscriberNumber = "0000000";

            int id = await clientService.AddAsync(
                name,
                codeOfTheCountry,
                regionCode,
                subscriberNumber,
                cancellationToken);

            var client = await clientService.SelectingAsync(id);

            // Act

            await clientService.DeleteAsync(client, cancellationToken);

            // Assert

            Assert.Empty(context.Clients);
        }
    }
}
