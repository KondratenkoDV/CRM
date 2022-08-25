using System;
using Xunit;
using Application.Services.Client;
using Domain.Enum;

namespace Application.Tests
{
    public class ClientServiceTests : TestCommandBase
    {
        private (string, CodeOfTheCountry, string, string) GetParameters()
        {
            var name = "name";

            var codeOfTheCountry = CodeOfTheCountry.Ukraine;

            var regionCode = "00";

            var subscriberNumber = "0000000";

            return (name, codeOfTheCountry, regionCode, subscriberNumber);
        }

        [Fact]
        public async void Task_When_AddToDb_Expect_ClientWasAddedToDb()
        {
            // Arrange

            var clientService = new ClientService(Context);

            // Act

            await clientService.AddAsync(
                GetParameters().Item1,
                GetParameters().Item2,
                GetParameters().Item3,
                GetParameters().Item4,
                CancellationToken.None);

            // Assert

            Assert.NotNull(Context.Clients);
        }

        [Fact]
        public async void Task_When_SelectingAsync_Expect_ClientWasSelectFromDb()
        {
            // Arrenge

            var clientService = new ClientService(Context);

            int id = await clientService.AddAsync(
                GetParameters().Item1,
                GetParameters().Item2,
                GetParameters().Item3,
                GetParameters().Item4,
                CancellationToken.None);

            // Act

            var client = await clientService.SelectingAsync(id);

            // Assert

            Assert.Equal(id, client.Id);
        }

        [Fact]
        public async void Task_When_UpdateAsync_Expect_ClientWasUpdateInDb()
        {
            // Arrenge

            var clientService = new ClientService(Context);

            int id = await clientService.AddAsync(
                GetParameters().Item1,
                GetParameters().Item2,
                GetParameters().Item3,
                GetParameters().Item4,
                CancellationToken.None);

            var client = await clientService.SelectingAsync(id);

            var newName = "newName";

            var newRegionCode = "11";

            var newSubscriberNumber = "1111111";

            // Act

            await clientService.UpdateAsync(
                client,
                newName,
                GetParameters().Item2,
                newRegionCode,
                newSubscriberNumber,
                CancellationToken.None);

            // Assert

            Assert.Equal(newName, Context.Clients.Single(c => c.Id == id).Name);
            Assert.Equal(newRegionCode, Context.Clients.Single(c => c.Id == id).RegionCode);
            Assert.Equal(newSubscriberNumber, Context.Clients.Single(c => c.Id == id).SubscriberNumber);
        }

        [Fact]
        public async void Task_When_DeleteAsync_Expect_ClientWasDeleteInDb()
        {
            // Arrenge

            var clientService = new ClientService(Context);

            int id = await clientService.AddAsync(
                GetParameters().Item1,
                GetParameters().Item2,
                GetParameters().Item3,
                GetParameters().Item4,
                CancellationToken.None);

            var client = await clientService.SelectingAsync(id);

            // Act

            await clientService.DeleteAsync(client, CancellationToken.None);

            // Assert

            Assert.Empty(Context.Clients);
        }

        [Fact]
        public async void Task_When_AllAsync_Expect_ClientsWasSelectFromDb()
        {
            // Arrenge

            var clientService = new ClientService(Context);

            await clientService.AddAsync(
                GetParameters().Item1,
                GetParameters().Item2,
                GetParameters().Item3,
                GetParameters().Item4,
                CancellationToken.None);

            // Act

            var clients = await clientService.AllAsync();

            // Assert

            Assert.NotEmpty(clients);
        }
    }
}
