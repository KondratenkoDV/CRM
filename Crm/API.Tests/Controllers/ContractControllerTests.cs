using System;
using API.Tests.Connection;
using API.Controllers;
using API.DTOs.Contract;
using Xunit;

namespace API.Tests.Controllers
{
    public class ContractControllerTests
    {
        [Fact]
        public async void Task_When_CreateNewContract_Expect_CreateNewContractWasAddedToDb()
        {
            // Arrange

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var contractController = new ContractController(context);

            var createContractDto = new CreateContractDto()
            {
                Subject = "Subject",
                Address = "Address",
                Price = 0,
                ClientId = 0
            };

            // Act

            await contractController.CreateNewContract(createContractDto, cancellationToken);

            // Assert

            Assert.NotNull(context.Contracts);
        }

        [Fact]
        public async void Task_When_SelectingContract_Expect_SelectingContractWasAddedFromDb()
        {
            // Arrange

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var contractController = new ContractController(context);

            var createContractDto = new CreateContractDto()
            {
                Subject = "Subject",
                Address = "Address",
                Price = 0,
                ClientId = 0
            };

            var id = await contractController.CreateNewContract(createContractDto, cancellationToken);

            // Act

            var contract = await contractController.SelectingContract(id.Value);

            // Assert

            Assert.Equal(id, contract.Value.Id);
        }

        [Fact]
        public async void Task_When_UpdateContract_Expect_UpdateContractWasAddedInDb()
        {
            // Arrange

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var contractController = new ContractController(context);

            var createContractDto = new CreateContractDto()
            {
                Subject = "Subject",
                Address = "Address",
                Price = 0,
                ClientId = 0
            };

            var updateContractDto = new UpdateContractDto()
            {
                NewSubject = "NewSubject",
                NewAddress = "NewAddress",
                NewPrice = 1,
                NewClientId = 1
            };

            var id = await contractController.CreateNewContract(createContractDto, cancellationToken);

            // Act

            await contractController.UpdateContract(updateContractDto, id.Value, cancellationToken);

            // Assert

            Assert.Equal(updateContractDto.NewSubject, context.Contracts.Single(c => c.Id == id.Value).Subject);
            Assert.Equal(updateContractDto.NewAddress, context.Contracts.Single(c => c.Id == id.Value).Address);
            Assert.Equal(updateContractDto.NewPrice, context.Contracts.Single(c => c.Id == id.Value).Price);
            Assert.Equal(updateContractDto.NewClientId, context.Contracts.Single(c => c.Id == id.Value).ClientId);
        }

        [Fact]
        public async void Task_When_DeleteContract_Expect_DeleteContractWasAddedFromDb()
        {
            // Arrange

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var contractController = new ContractController(context);

            var createContractDto = new CreateContractDto()
            {
                Subject = "Subject",
                Address = "Address",
                Price = 0,
                ClientId = 0
            };

            var id = await contractController.CreateNewContract(createContractDto, cancellationToken);

            // Act

            await contractController.DeleteContract(id.Value, cancellationToken);

            // Assert

            Assert.Empty(context.Contracts);
        }
    }
}
