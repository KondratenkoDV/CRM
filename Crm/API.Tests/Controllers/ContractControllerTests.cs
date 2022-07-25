using System;
using API.Tests.Connection;
using API.Controllers;
using API.DTOs.Contract;
using Xunit;
using Microsoft.AspNetCore.Mvc;

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

            var value = await contractController.CreateNewContract(createContractDto, cancellationToken);

            var result = value.Result as OkObjectResult;

            int id = (int)result.Value;

            // Act

            var contractValue = await contractController.SelectingContract(id);

            var contractResult = contractValue.Result as OkObjectResult;

            var contract = (SelectingContractDto)contractResult.Value;

            // Assert

            Assert.Equal(id, contract.Id);
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

            var value = await contractController.CreateNewContract(createContractDto, cancellationToken);

            var result = value.Result as OkObjectResult;

            int id = (int)result.Value;

            // Act

            await contractController.UpdateContract(updateContractDto, id, cancellationToken);

            // Assert

            Assert.Equal(updateContractDto.NewSubject, context.Contracts.Single(c => c.Id == id).Subject);
            Assert.Equal(updateContractDto.NewAddress, context.Contracts.Single(c => c.Id == id).Address);
            Assert.Equal(updateContractDto.NewPrice, context.Contracts.Single(c => c.Id == id).Price);
            Assert.Equal(updateContractDto.NewClientId, context.Contracts.Single(c => c.Id == id).ClientId);
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

            var value = await contractController.CreateNewContract(createContractDto, cancellationToken);

            var result = value.Result as OkObjectResult;

            int id = (int)result.Value;

            // Act

            await contractController.DeleteContract(id, cancellationToken);

            // Assert

            Assert.Empty(context.Contracts);
        }
    }
}
