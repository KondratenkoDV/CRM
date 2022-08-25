using System;
using Xunit;
using API.Controllers;
using API.DTOs.Client;
using Domain.Enum;
using Moq;
using Domain.Interfaces;

namespace API.Tests.Controllers
{
    public class ClientControllerTests
    {
        private int CreateClient(Mock<IClientService> mock)
        {
            return mock.Object.AddAsync(
                "Name",
                CodeOfTheCountry.Ukraine,
                "00",
                "0000000",
                CancellationToken.None).Result;
        }

        [Fact]
        public async void Task_When_CreateNewClient_Expect_ClientWasCreated()
        {
            // Arrange

            var createClientDto = new CreateClientDto()
            {
                Name = "Name",
                СodeOfTheCountry = CodeOfTheCountry.Ukraine,
                RegionCode = "00",
                SubscriberNumber = "0000000"
            };

            var mock = new Mock<IClientService>();

            mock.Setup(c => c.AddAsync(
                createClientDto.Name,
                createClientDto.СodeOfTheCountry,
                createClientDto.RegionCode,
                createClientDto.SubscriberNumber,
                CancellationToken.None)).Returns(It.IsAny<Task<int>>);            

            var clientController = new ClientController(mock.Object);
            
            // Act

            await clientController.CreateNewClient(createClientDto, CancellationToken.None);

            // Assert

            mock.Verify(c => c.AddAsync(
                createClientDto.Name,
                createClientDto.СodeOfTheCountry,
                createClientDto.RegionCode,
                createClientDto.SubscriberNumber,
                CancellationToken.None),
                Times.Once());
        }

        [Fact]
        public async void Task_When_SelectingClient_Expect_ClientWasSelected()
        {
            // Arrange

            var mock = new Mock<IClientService>();

            var id = CreateClient(mock);

            mock.Setup(c => c.SelectingAsync(id))
                .Returns(It.IsAny<Task<Domain.Client>>);

            var clientController = new ClientController(mock.Object);

            // Act

            await clientController.SelectingClient(id);

            // Assert

            mock.Verify(c => c.SelectingAsync(id), Times.Once());
        }

        [Fact]
        public async void Task_When_UpdateClient_Expect_ClientWasUpdate()
        {
            // Arrange

            //var client = CreateClient();

            var updateClientDto = new UpdateClientDto()
            {
                NewName = "NewName",
                NewСodeOfTheCountry = CodeOfTheCountry.Ukraine,
                NewRegionCode = "11",
                NewSubscriberNumber = "1111111"
            };

            var mock = new Mock<IClientService>();

            var id = CreateClient(mock);

            var client = mock.Object.SelectingAsync(id).Result;

            mock.Setup(c => c.UpdateAsync(
                client,
                updateClientDto.NewName,
                updateClientDto.NewСodeOfTheCountry,
                updateClientDto.NewRegionCode,
                updateClientDto.NewSubscriberNumber,
                CancellationToken.None));

            var clientController = new ClientController(mock.Object);
            
            // Act

            await clientController.UpdateClient(updateClientDto, id, CancellationToken.None);

            // Assert

            mock.Verify(c => c.UpdateAsync(
                client,
                updateClientDto.NewName,
                updateClientDto.NewСodeOfTheCountry,
                updateClientDto.NewRegionCode,
                updateClientDto.NewSubscriberNumber,
                CancellationToken.None), Times.Once());
        }

        [Fact]
        public async void Task_When_DeleteClient_Expect_ClientWasDeleted()
        {
            // Arrange

            var mock = new Mock<IClientService>();

            var id = CreateClient(mock);

            var client = mock.Object.SelectingAsync(id).Result;

            mock.Setup(c => c.DeleteAsync(client, CancellationToken.None));

            var clientController = new ClientController(mock.Object);

            // Act

            await clientController.DeleteClient(id, CancellationToken.None);

            // Assert

            mock.Verify(c => c.DeleteAsync(client, CancellationToken.None), Times.Once());
        }

        [Fact]
        public async void Task_When_GetClients_Expect_ClientsWasSelected()
        {
            // Arrange

            var mock = new Mock<IClientService>();

            CreateClient(mock);

            mock.Setup(c => c.AllAsync())
                .Returns(It.IsAny<Task<IEnumerable<Domain.Client>>>);

            var clientController = new ClientController(mock.Object);

            // Act

            await clientController.GetClients();

            // Assert

            mock.Verify(c => c.AllAsync(), Times.Once());
        }
    }
}
