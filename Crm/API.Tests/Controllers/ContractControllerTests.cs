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
        private int CreateContract(Mock<IContractService> mock)
        {
            return mock.Object.AddAsync(
                "Subject",
                "Address",
                0,
                0,
                CancellationToken.None).Result;
        }

        [Fact]
        public async void Task_When_CreateNewContract_Expect_ContractWasCreated()
        {
            // Arrange

            var createContractDto = new CreateContractDto()
            {
                Subject = "Subject",
                Address = "Address",
                Price = 0,
                ClientId = 0
            };

            var mock = new Mock<IContractService>();

            mock.Setup(c => c.AddAsync(
                createContractDto.Subject,
                createContractDto.Address,
                createContractDto.Price,
                createContractDto.ClientId,
                CancellationToken.None)).Returns(It.IsAny<Task<int>>());

            var contractController = new ContractController(mock.Object);            

            // Act

            await contractController.CreateNewContract(createContractDto, CancellationToken.None);

            // Assert

            mock.Verify(c => c.AddAsync(
                createContractDto.Subject,
                createContractDto.Address,
                createContractDto.Price,
                createContractDto.ClientId,
                CancellationToken.None),
                Times.Once());
        }

        [Fact]
        public async void Task_When_SelectingContract_Expect_ContractWasSelected()
        {
            // Arrange

            var mock = new Mock<IContractService>();

            var id = CreateContract(mock);

            mock.Setup(c => c.SelectingAsync(id))
                .Returns(It.IsAny<Task<Domain.Contract>>());

            var contractController = new ContractController(mock.Object);

            // Act

            await contractController.SelectingContract(id);

            // Assert

            mock.Verify(c => c.SelectingAsync(id), Times.Once());
        }

        [Fact]
        public async void Task_When_UpdateContract_Expect_ContractWasUpdate()
        {
            // Arrange

            var updateContractDto = new UpdateContractDto()
            {
                NewSubject = "NewSubject",
                NewAddress = "NewAddress",
                NewPrice = 1,
                NewClientId = 1
            };

            var mock = new Mock<IContractService>();

            var id = CreateContract(mock);

            var contract = mock.Object.SelectingAsync(id).Result;

            mock.Setup(c => c.UpdateAsync(
                contract,
                updateContractDto.NewSubject,
                updateContractDto.NewAddress,
                updateContractDto.NewPrice,
                updateContractDto.NewClientId,
                CancellationToken.None));

            var contractController = new ContractController(mock.Object);

            // Act

            await contractController.UpdateContract(updateContractDto, id, CancellationToken.None);

            // Assert

            mock.Verify(c => c.UpdateAsync(
                contract,
                updateContractDto.NewSubject,
                updateContractDto.NewAddress,
                updateContractDto.NewPrice,
                updateContractDto.NewClientId,
                CancellationToken.None),
                Times.Once());
        }

        [Fact]
        public async void Task_When_DeleteContract_Expect_ContractWasDeleted()
        {
            // Arrange

            var mock = new Mock<IContractService>();

            var id = CreateContract(mock);

            var contract = mock.Object.SelectingAsync(id).Result;

            mock.Setup(c => c.DeleteAsync(contract, CancellationToken.None));

            var contractController = new ContractController(mock.Object);

            // Act

            await contractController.DeleteContract(7, CancellationToken.None);

            // Assert

            mock.Verify(c => c.DeleteAsync(
                contract,
                CancellationToken.None),
                Times.Once());
        }
    }
}
