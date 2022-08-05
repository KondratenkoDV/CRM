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
        [Fact]
        public async void Task_When_CreateNewClient_Expect_ClientWasCreated()
        {
            // Arrange

            var mock = new Mock<IClientService>();

            mock.Setup(c => c.AddAsync(
                "Test",
                CodeOfTheCountry.Ukraine,
                "00",
                "0000000",
                CancellationToken.None)).Returns(It.IsAny<Task<int>>);

            var createClientDto = new CreateClientDto()
            {
                Name = "Name",
                СodeOfTheCountry = CodeOfTheCountry.Ukraine,
                RegionCode = "00",
                SubscriberNumber = "0000000"
            };

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
            
            mock.Setup(c => c.SelectingAsync(It.IsAny<int>()))
                .Returns(It.IsAny<Task<Domain.Client>>);

            var clientController = new ClientController(mock.Object);

            // Act

            await clientController.SelectingClient(It.IsAny<int>());

            // Assert

            mock.Verify(c => c.SelectingAsync(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async void Task_When_UpdateClient_Expect_ClientWasUpdate()
        {
            // Arrange

            var mock = new Mock<IClientService>();
            
            mock.Setup(c => c.UpdateAsync(
                It.IsAny<Domain.Client>(),
                It.IsAny<string>(),
                It.IsAny<CodeOfTheCountry>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                CancellationToken.None));

            var clientController = new ClientController(mock.Object);

            var updateClientDto = new UpdateClientDto()
            {
                NewName = "NewName",
                NewСodeOfTheCountry = CodeOfTheCountry.Ukraine,
                NewRegionCode = "00",
                NewSubscriberNumber = "0000000"
            };

            // Act

            await clientController.UpdateClient(updateClientDto, It.IsAny<int>(), CancellationToken.None);

            // Assert

            mock.Verify(c => c.UpdateAsync(
                It.IsAny<Domain.Client>(),
                It.IsAny<string>(),
                It.IsAny<CodeOfTheCountry>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                CancellationToken.None), Times.Once());
        }

        [Fact]
        public async void Task_When_DeleteClient_Expect_ClientWasDeleted()
        {
            // Arrange

            var mock = new Mock<IClientService>();

            mock.Setup(c => c.DeleteAsync(It.IsAny<Domain.Client>(), CancellationToken.None));

            var clientController = new ClientController(mock.Object);

            // Act

            await clientController.DeleteClient(It.IsAny<int>(), CancellationToken.None);

            // Assert

            mock.Verify(c => c.DeleteAsync(It.IsAny<Domain.Client>(), CancellationToken.None), Times.Once());
        }
    }
}
