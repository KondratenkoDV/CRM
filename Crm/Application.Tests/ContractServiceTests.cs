using System;
using Xunit;
using Application.Tests.Connection;
using Application.Services.Contract;

namespace Application.Tests
{
    public class ContractServiceTests
    {
        [Fact]
        public async void Task_When_AddAsync_Expect_ContractWasAddedToDb()
        {
            // Arrenge

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var contractService = new ContractService(context);

            var subject = "subject";

            var address = "address";

            var price = 0;

            var clientId = 0;

            // Act

            await contractService.AddAsync(
                subject,
                address,
                price,
                clientId,
                cancellationToken);

            // Assert

            Assert.NotNull(context.Contracts);
        }

        [Fact]
        public async void Task_When_SelectingAsync_Expect_ContractWasSelectFromDb()
        {
            // Arrenge

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var contractService = new ContractService(context);

            var subject = "subject";

            var address = "address";

            var price = 0;

            var clientId = 0;

            int id = await contractService.AddAsync(
                subject,
                address,
                price,
                clientId,
                cancellationToken);

            // Act

            var contract = await contractService.SelectingAsync(id);

            // Assert

            Assert.Equal(id, contract.Id);
        }

        [Fact]
        public async void Task_When_UpdateAsync_Expect_ContractWasUpdateInDb()
        {
            // Arrenge

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var contractService = new ContractService(context);

            var subject = "subject";

            var address = "address";

            var price = 0;

            var clientId = 0;

            int id = await contractService.AddAsync(
                subject,
                address,
                price,
                clientId,
                cancellationToken);

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
                cancellationToken);

            // Assert

            Assert.Equal(newSubject, context.Contracts.Single(c => c.Id == id).Subject);
            Assert.Equal(newAddress, context.Contracts.Single(c => c.Id == id).Address);
            Assert.Equal(newPrice, context.Contracts.Single(c => c.Id == id).Price);
            Assert.Equal(newClientId, context.Contracts.Single(c => c.Id == id).ClientId);
        }

        [Fact]
        public async void Task_When_DeleteAsync_Expect_ContracWasDeleteInDb()
        {
            // Arrenge

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var contractService = new ContractService(context);

            var subject = "subject";

            var address = "address";

            var price = 0;

            var clientId = 0;

            int id = await contractService.AddAsync(
                subject,
                address,
                price,
                clientId,
                cancellationToken);

            var contract = await contractService.SelectingAsync(id);

            // Act

            await contractService.DeleteAsync(contract, cancellationToken);

            // Assert

            Assert.Empty(context.Contracts);
        }
    }
}
