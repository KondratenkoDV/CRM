using System;
using API.Controllers;
using API.DTOs.Contract;
using Xunit;
using Moq;
using Domain.Interfaces;

namespace API.Tests.Controllers
{
    public class ContractControllerTests
    {
        [Fact]
        public async void Task_When_CreateNewContract_Expect_ContractWasCreated()
        {
            // Arrange

            var mock = new Mock<IContractService>();

            mock.Setup(c => c.AddAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<int>(),
                CancellationToken.None)).Returns(It.IsAny<Task<int>>());

            var contractController = new ContractController(mock.Object);

            var createContractDto = new CreateContractDto()
            {
                Subject = "Subject",
                Address = "Address",
                Price = 0,
                ClientId = 0
            };

            // Act

            await contractController.CreateNewContract(createContractDto, CancellationToken.None);

            // Assert

            mock.Verify(c => c.AddAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<int>(),
                CancellationToken.None),
                Times.Once());
        }

        [Fact]
        public async void Task_When_SelectingContract_Expect_ContractWasSelected()
        {
            // Arrange

            var mock = new Mock<IContractService>();

            mock.Setup(c => c.SelectingAsync(It.IsAny<int>()))
                .Returns(It.IsAny<Task<Domain.Contract>>());

            var contractController = new ContractController(mock.Object);

            // Act

            await contractController.SelectingContract(It.IsAny<int>());

            // Assert

            mock.Verify(c => c.SelectingAsync(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async void Task_When_UpdateContract_Expect_ContractWasUpdate()
        {
            // Arrange

            var mock = new Mock<IContractService>();

            mock.Setup(c => c.UpdateAsync(
                It.IsAny<Domain.Contract>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<int>(),
                CancellationToken.None));

            var contractController = new ContractController(mock.Object);

            var updateContractDto = new UpdateContractDto()
            {
                NewSubject = "NewSubject",
                NewAddress = "NewAddress",
                NewPrice = 1,
                NewClientId = 1
            };

            // Act

            await contractController.UpdateContract(updateContractDto, It.IsAny<int>(), CancellationToken.None);

            // Assert

            mock.Verify(c => c.UpdateAsync(
                It.IsAny<Domain.Contract>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<int>(),
                CancellationToken.None),
                Times.Once());
        }

        [Fact]
        public async void Task_When_DeleteContract_Expect_ContractWasDeleted()
        {
            // Arrange

            var mock = new Mock<IContractService>();

            mock.Setup(c => c.DeleteAsync(It.IsAny<Domain.Contract>(), CancellationToken.None));

            var contractController = new ContractController(mock.Object);

            // Act

            await contractController.DeleteContract(It.IsAny<int>(), CancellationToken.None);

            // Assert

            mock.Verify(c => c.DeleteAsync(
                It.IsAny<Domain.Contract>(),
                CancellationToken.None),
                Times.Once());
        }
    }
}
