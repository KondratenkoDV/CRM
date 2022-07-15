using System;
using Xunit;
using Crm.Application.Crud.Contract;

namespace Crm.Application.Tests
{
    public class ContractCrudTests
    {
        [Fact]
        public async void Task_When_AddToDbAsync_Expect_ContractWasAddedToDb()
        {
            // Arrenge

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var contractCrud = new ContractCrud(context);

            var contractParameters = new ContractParameters()
            {
                Subject = "Subject",
                Address = "Address",
                Price = 0
            };

            // Act

            await contractCrud.AddToDbAsync(contractParameters, cancellationToken);

            // Assert

            Assert.NotNull(context.Contracts);
        }

        [Fact]
        public async void Task_When_SelectingFromTheDbAsync_Expect_ContractWasSelectFromDb()
        {
            // Arrenge

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var contractCrud = new ContractCrud(context);

            var contractParameters = new ContractParameters()
            {
                Subject = "Subject",
                Address = "Address",
                Price = 0
            };

            int id = await contractCrud.AddToDbAsync(contractParameters, cancellationToken);

            // Act

            var contract = await contractCrud.SelectingFromTheDbAsync(id);

            // Assert

            Assert.Equal(id, contract.Id);
        }

        [Fact]
        public async void Task_When_UpdateInTheDbAsync_Expect_ContractWasUpdateInDb()
        {
            // Arrenge

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var contractCrud = new ContractCrud(context);

            var contractParameters = new ContractParameters()
            {
                Subject = "Subject",
                Address = "Address",
                Price = 0
            };

            int id = await contractCrud.AddToDbAsync(contractParameters, cancellationToken);

            var newSubject = "newSubject";

            var newContractParameters = new ContractParameters()
            {
                Subject = newSubject,
                Address = "Address",
                Price = 0
            };

            // Act

            await contractCrud.UpdateInTheDbAsync(newContractParameters, id, cancellationToken);

            // Assert

            Assert.Equal(newSubject, context.Contracts.Single(c => c.Id == id).Subject);
        }

        [Fact]
        public async void Task_When_DeleteInTheDbAsync_Expect_ContracWasDeleteInDb()
        {
            // Arrenge

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var contractCrud = new ContractCrud(context);

            var contractParameters = new ContractParameters()
            {
                Subject = "Subject",
                Address = "Address",
                Price = 0
            };

            int id = await contractCrud.AddToDbAsync(contractParameters, cancellationToken);

            // Act

            await contractCrud.DeleteInTheDbAsync(id, cancellationToken);

            // Assert

            Assert.Empty(context.Contracts);
        }
    }
}
