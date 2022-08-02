using System;
using API.Controllers;
using API.DTOs.Contract;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Application.Services.Contract;

namespace API.Tests.Controllers
{
    public class ContractControllerTests : TestCommandBase
    {
        private CreateContractDto GetCreateContractDto()
        {
            return new CreateContractDto()
            {
                Subject = "Subject",
                Address = "Address",
                Price = 0,
                ClientId = 0
            };
        }

        [Fact]
        public async void Task_When_CreateNewContract_Expect_ContractWasCreated()
        {
            // Arrange

            var mock = new Mock<ContractService>(Context);

            var contractController = new ContractController(mock.Object);

            var createContractDto = GetCreateContractDto();

            // Act

            await contractController.CreateNewContract(createContractDto, CancellationToken.None);

            // Assert

            Assert.NotNull(Context.Contracts);
        }

        [Fact]
        public async void Task_When_SelectingContract_Expect_ContractWasSelected()
        {
            // Arrange

            var mock = new Mock<ContractService>(Context);

            var contractController = new ContractController(mock.Object);

            var createContractDto = GetCreateContractDto();

            var value = await contractController.CreateNewContract(createContractDto, CancellationToken.None);

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
        public async void Task_When_UpdateContract_Expect_ContractWasUpdate()
        {
            // Arrange

            var mock = new Mock<ContractService>(Context);

            var contractController = new ContractController(mock.Object);

            var createContractDto = GetCreateContractDto();

            var updateContractDto = new UpdateContractDto()
            {
                NewSubject = "NewSubject",
                NewAddress = "NewAddress",
                NewPrice = 1,
                NewClientId = 1
            };

            var value = await contractController.CreateNewContract(createContractDto, CancellationToken.None);

            var result = value.Result as OkObjectResult;

            int id = (int)result.Value;

            // Act

            await contractController.UpdateContract(updateContractDto, id, CancellationToken.None);

            // Assert

            Assert.Equal(updateContractDto.NewSubject, Context.Contracts.Single(c => c.Id == id).Subject);
            Assert.Equal(updateContractDto.NewAddress, Context.Contracts.Single(c => c.Id == id).Address);
            Assert.Equal(updateContractDto.NewPrice, Context.Contracts.Single(c => c.Id == id).Price);
            Assert.Equal(updateContractDto.NewClientId, Context.Contracts.Single(c => c.Id == id).ClientId);
        }

        [Fact]
        public async void Task_When_DeleteContract_Expect_ContractWasDeleted()
        {
            // Arrange

            var mock = new Mock<ContractService>(Context);

            var contractController = new ContractController(mock.Object);

            var createContractDto = GetCreateContractDto();

            var value = await contractController.CreateNewContract(createContractDto, CancellationToken.None);

            var result = value.Result as OkObjectResult;

            int id = (int)result.Value;

            // Act

            await contractController.DeleteContract(id, CancellationToken.None);

            // Assert

            Assert.Empty(Context.Contracts);
        }
    }
}
