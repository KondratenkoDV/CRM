using System;
using Xunit;
using Crm.Application.Crud.Client;

namespace Crm.Application.Tests
{
    public class ClientCrudTests
    {
        [Fact]
        public async void Task_When_AddToDb_Expect_ClientWasAddedToDb()
        {
            // Arrenge

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var clientCrud = new ClientCrud(context);

            var clientParameter = new ClientParameters()
            {
                Name = "name",
                СodeOfTheCountry = Domain.CodeOfTheCountry.Ukraine,
                RegionCode = "00",
                SubscriberNumber = "0000000"
            };

            // Act

            await clientCrud.AddToDbAsync(clientParameter, cancellationToken);

            // Assert

            Assert.NotNull(context.Clients);
        }

        [Fact]
        public async void Task_When_SelectingFromTheDbAsync_Expect_ClientWasSelectFromDb()
        {
            // Arrenge

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var clientCrud = new ClientCrud(context);

            var clientParameter = new ClientParameters()
            {
                Name = "name",
                СodeOfTheCountry = Domain.CodeOfTheCountry.Ukraine,
                RegionCode = "00",
                SubscriberNumber = "0000000"
            };

            int id = await clientCrud.AddToDbAsync(clientParameter, cancellationToken);

            // Act

            var client = await clientCrud.SelectingFromTheDbAsync(id);

            // Assert

            Assert.Equal(id, client.Id);
        }

        [Fact]
        public async void Task_When_UpdateInTheDbAsync_Expect_ClientWasUpdateInDb()
        {
            // Arrenge

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var clientCrud = new ClientCrud(context);

            var clientParameter = new ClientParameters()
            {
                Name = "name",
                СodeOfTheCountry = Domain.CodeOfTheCountry.Ukraine,
                RegionCode = "00",
                SubscriberNumber = "0000000"
            };

            int id = await clientCrud.AddToDbAsync(clientParameter, cancellationToken);

            var newName = "newName";

            var newClientParameter = new ClientParameters()
            {
                Name = newName,
                СodeOfTheCountry = Domain.CodeOfTheCountry.Ukraine,
                RegionCode = "00",
                SubscriberNumber = "0000000"
            };

            // Act

            await clientCrud.UpdateInTheDbAsync(newClientParameter, id, cancellationToken);

            // Assert

            Assert.Equal(newName, context.Clients.Single(c => c.Id == id).Name);            
        }

        [Fact]
        public async void Task_When_DeleteInTheDbAsync_Expect_ClientWasDeleteInDb()
        {
            // Arrenge

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var clientCrud = new ClientCrud(context);

            var clientParameter = new ClientParameters()
            {
                Name = "name",
                СodeOfTheCountry = Domain.CodeOfTheCountry.Ukraine,
                RegionCode = "00",
                SubscriberNumber = "0000000"
            };

            int id = await clientCrud.AddToDbAsync(clientParameter, cancellationToken);

            // Act

            await clientCrud.DeleteInTheDbAsync(id, cancellationToken);

            // Assert

            Assert.Empty(context.Clients);
        }
    }
}
