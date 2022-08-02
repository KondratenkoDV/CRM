using System;
using Xunit;
using API.Controllers;
using API.DTOs.Client;
using Domain.Enum;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Application.Services.Client;

namespace API.Tests.Controllers
{
    public class ClientControllerTests : TestCommandBase
    {
        private CreateClientDto GetCreateClientDto()
        {
            return new CreateClientDto()
            {
                Name = "Name",
                СodeOfTheCountry = CodeOfTheCountry.Ukraine,
                RegionCode = "00",
                SubscriberNumber = "0000000"
            };
        }

        [Fact]
        public async void Task_When_CreateNewClient_Expect_ClientWasCreated()
        {
            // Arrange

            var mock = new Mock<ClientService>(Context);

            var clientController = new ClientController(mock.Object);

            var createClientDto = GetCreateClientDto();

            // Act

            await clientController.CreateNewClient(createClientDto, CancellationToken.None);

            // Assert

            Assert.NotNull(Context.Clients);
        }

        [Fact]
        public async void Task_When_SelectingClient_Expect_ClientWasSelected()
        {
            // Arrange

            var mock = new Mock<ClientService>(Context);

            var clientController = new ClientController(mock.Object);

            var createClientDto = GetCreateClientDto();

            var value = await clientController.CreateNewClient(createClientDto, CancellationToken.None);

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

            var mock = new Mock<ClientService>(Context);

            var clientController = new ClientController(mock.Object);

            var createClientDto = GetCreateClientDto();

            var updateClientDto = new UpdateClientDto()
            {
                NewName = "NewName",
                NewСodeOfTheCountry = CodeOfTheCountry.Ukraine,
                NewRegionCode = "00",
                NewSubscriberNumber = "0000000"
            };

            var value = await clientController.CreateNewClient(createClientDto, CancellationToken.None);

            var result = value.Result as OkObjectResult;

            int id = (int)result.Value;

            // Act

            await clientController.UpdateClient(updateClientDto, id, CancellationToken.None);

            // Assert

            Assert.Equal(updateClientDto.NewName, Context.Clients.Single(c => c.Id == id).Name);
            Assert.Equal(updateClientDto.NewRegionCode, Context.Clients.Single(c => c.Id == id).RegionCode);
            Assert.Equal(updateClientDto.NewSubscriberNumber, Context.Clients.Single(c => c.Id == id).SubscriberNumber);
        }

        [Fact]
        public async void Task_When_DeleteClient_Expect_ClientWasDeleted()
        {
            // Arrange

            var mock = new Mock<ClientService>(Context);

            var clientController = new ClientController(mock.Object);

            var createClientDto = GetCreateClientDto();

            var value = await clientController.CreateNewClient(createClientDto, CancellationToken.None);

            var result = value.Result as OkObjectResult;

            int id = (int)result.Value;

            // Act

            await clientController.DeleteClient(id, CancellationToken.None);

            // Assert

            Assert.Empty(Context.Clients);
        }
    }
}
