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
        public async void Task_When_CreateNewClient_Expect_ClientWasCreated()
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
        public async void Task_When_SelectingClient_Expect_ClientWasSelected()
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

            var value = await clientController.CreateNewClient(createClientDto, cancellationToken);

            var result = value.Result as OkObjectResult;
                        
            int id = (int)result.Value;
         
            // Act

            var clientValue = await clientController.SelectingClient(id);

            var clientResult = clientValue.Result as OkObjectResult;

            var client = (SelectingClientDto)clientResult.Value;

            // Assert

            Assert.Equal(id, client.Id);
        }

        [Fact]
        public async void Task_When_UpdateClient_Expect_ClientWasUpdate()
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

            var value = await clientController.CreateNewClient(createClientDto, cancellationToken);

            var result = value.Result as OkObjectResult;

            int id = (int)result.Value;

            // Act

            await clientController.UpdateClient(updateClientDto, id, cancellationToken);

            // Assert

            Assert.Equal(updateClientDto.NewName, context.Clients.Single(c => c.Id == id).Name);
            Assert.Equal(updateClientDto.NewRegionCode, context.Clients.Single(c => c.Id == id).RegionCode);
            Assert.Equal(updateClientDto.NewSubscriberNumber, context.Clients.Single(c => c.Id == id).SubscriberNumber);
        }

        [Fact]
        public async void Task_When_DeleteClient_Expect_ClientWasDeleted()
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

            var value = await clientController.CreateNewClient(createClientDto, cancellationToken);

            var result = value.Result as OkObjectResult;

            int id = (int)result.Value;

            // Act

            await clientController.DeleteClient(id, cancellationToken);

            // Assert

            Assert.Empty(context.Clients);
        }
    }
}
