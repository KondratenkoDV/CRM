using System;
using Xunit;
using Application.Services.Contract;

namespace Application.Tests
{
    public class ContractServiceTests : TestCommandBase
    {
        private (string, string, int, int) GetParameters()
        {
            var subject = "subject";

            var address = "address";

            var price = 0;

            var clientId = 0;

            return (subject, address, price, clientId);
        }

        [Fact]
        public async void Task_When_AddAsync_Expect_ContractWasAddedToDb()
        {
            // Arrange

            var contractService = new ContractService(Context);

            // Act

            await contractService.AddAsync(
                GetParameters().Item1,
                GetParameters().Item2,
                GetParameters().Item3,
                GetParameters().Item4,
                CancellationToken.None);

            // Assert

            Assert.NotNull(Context.Contracts);
        }

        [Fact]
        public async void Task_When_SelectingAsync_Expect_ContractWasSelectFromDb()
        {
            // Arrenge

            var contractService = new ContractService(Context);

            int id = await contractService.AddAsync(
                GetParameters().Item1,
                GetParameters().Item2,
                GetParameters().Item3,
                GetParameters().Item4,
                CancellationToken.None);

            // Act

            var contract = await contractService.SelectingAsync(id);

            // Assert

            Assert.Equal(id, contract.Id);
        }

        [Fact]
        public async void Task_When_UpdateAsync_Expect_ContractWasUpdateInDb()
        {
            // Arrenge

            var contractService = new ContractService(Context);

            int id = await contractService.AddAsync(
                GetParameters().Item1,
                GetParameters().Item2,
                GetParameters().Item3,
                GetParameters().Item4,
                CancellationToken.None);

            var contract = await contractService.SelectingAsync(id);

            var newSubject = "newSubject";

            var newAddress = "newAddress";

            var newPrice = 1;

            var newClientId = 1;

            // Act

            await contractService.UpdateAsync(
                contract,
                newSubject,
                newAddress,
                newPrice,
                newClientId,
                CancellationToken.None);

            // Assert

            Assert.Equal(newSubject, Context.Contracts.Single(c => c.Id == id).Subject);
            Assert.Equal(newAddress, Context.Contracts.Single(c => c.Id == id).Address);
            Assert.Equal(newPrice, Context.Contracts.Single(c => c.Id == id).Price);
            Assert.Equal(newClientId, Context.Contracts.Single(c => c.Id == id).ClientId);
        }

        [Fact]
        public async void Task_When_DeleteAsync_Expect_ContracWasDeleteInDb()
        {
            // Arrenge

            var contractService = new ContractService(Context);

            int id = await contractService.AddAsync(
                GetParameters().Item1,
                GetParameters().Item2,
                GetParameters().Item3,
                GetParameters().Item4,
                CancellationToken.None);

            var contract = await contractService.SelectingAsync(id);

            // Act

            await contractService.DeleteAsync(contract, CancellationToken.None);

            // Assert

            Assert.Empty(Context.Contracts);
        }
    }
}
