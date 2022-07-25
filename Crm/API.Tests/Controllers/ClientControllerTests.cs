using System;
using Xunit;
using API.Tests.Connection;
using API.Controllers;
using API.DTOs.Client;
using Domain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace API.Tests.Controllers
{
    public class ClientControllerTests
    {
        [Fact]
        public async void Task_When_CreateNewClient_Expect_CreateNewClientWasAddedToDb()
        {
            // Arrange

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var clientController = new ClientController(context);

            var createClientDto = new CreateClientDto()
            {
                Name = "Name",
                СodeOfTheCountry = CodeOfTheCountry.Ukraine,
                RegionCode = "00",
                SubscriberNumber = "0000000"
            };

            // Act

            await clientController.CreateNewClient(createClientDto, cancellationToken);

            // Assert

            Assert.NotNull(context.Clients);
        }

        [Fact]
        public async void Task_When_SelectingClient_Expect_SelectingClientWasAddedFromDb()
        {
            // Arrange

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var clientController = new ClientController(context);

            var createClientDto = new CreateClientDto()
            {
                Name = "Name",
                СodeOfTheCountry = CodeOfTheCountry.Ukraine,
                RegionCode = "00",
                SubscriberNumber = "0000000"
            };

            var id = await clientController.CreateNewClient(createClientDto, cancellationToken);

            // Act

            var client = await clientController.SelectingClient(id.Value);

            // Assert

            Assert.Equal(id.Value, client.Value.Id);
        }

        [Fact]
        public async void Task_When_UpdateClient_Expect_UpdateClientWasAddedInDb()
        {
            // Arrange

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var clientController = new ClientController(context);

            var createClientDto = new CreateClientDto()
            {
                Name = "Name",
                СodeOfTheCountry = CodeOfTheCountry.Ukraine,
                RegionCode = "00",
                SubscriberNumber = "0000000"
            };

            var updateClientDto = new UpdateClientDto()
            {
                NewName = "NewName",
                NewСodeOfTheCountry = CodeOfTheCountry.Ukraine,
                NewRegionCode = "00",
                NewSubscriberNumber = "0000000"
            };

            var id = await clientController.CreateNewClient(createClientDto, cancellationToken);

            // Act

            await clientController.UpdateClient(updateClientDto, id.Value, cancellationToken);

            // Assert

            Assert.Equal(updateClientDto.NewName, context.Clients.Single(c => c.Id == id.Value).Name);
            Assert.Equal(updateClientDto.NewRegionCode, context.Clients.Single(c => c.Id == id.Value).RegionCode);
            Assert.Equal(updateClientDto.NewSubscriberNumber, context.Clients.Single(c => c.Id == id.Value).SubscriberNumber);
        }

        [Fact]
        public async void Task_When_DeleteClient_Expect_DeleteClientWasAddedFromDb()
        {
            // Arrange

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var clientController = new ClientController(context);

            var createClientDto = new CreateClientDto()
            {
                Name = "Name",
                СodeOfTheCountry = CodeOfTheCountry.Ukraine,
                RegionCode = "00",
                SubscriberNumber = "0000000"
            };

            var id = await clientController.CreateNewClient(createClientDto, cancellationToken);

            // Act

            await clientController.DeleteClient(id.Value, cancellationToken);

            // Assert

            Assert.Empty(context.Clients);
        }
    }
}
