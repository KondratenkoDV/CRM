using System;
using Xunit;
using Crm.Application.Crud.Position;

namespace Crm.Application.Tests
{
    public class PositionCrudTests
    {
        [Fact]
        public async void Task_When_AddToDbAsync_Expect_PositionWasAddedToDb()
        {
            // Arrenge

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var positionCrud = new PositionCrud(context);

            var positionParameters = new PositionParameters()
            {
                Name = "Name"
            };

            // Act

            await positionCrud.AddToDbAsync(positionParameters, cancellationToken);

            // Assert

            Assert.NotNull(context.Positions);
        }

        [Fact]
        public async void Task_When_SelectingFromTheDbAsync_Expect_PositionWasSelectFromDb()
        {
            // Arrenge

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var positionCrud = new PositionCrud(context);

            var positionParameters = new PositionParameters()
            {
                Name = "Name"
            };

            int id = await positionCrud.AddToDbAsync(positionParameters, cancellationToken);

            // Act

            var position = await positionCrud.SelectingFromTheDbAsync(id);

            // Assert

            Assert.Equal(id, position.Id);
        }

        [Fact]
        public async void Task_When_UpdateInTheDbAsync_Expect_PositionWasUpdateInDb()
        {
            // Arrenge

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var positionCrud = new PositionCrud(context);

            var positionParameters = new PositionParameters()
            {
                Name = "Name"
            };

            int id = await positionCrud.AddToDbAsync(positionParameters, cancellationToken);

            var newName = "newName";

            var newPositionParameters = new PositionParameters()
            {
                Name = newName
            };

            // Act

            await positionCrud.UpdateInTheDbAsync(newPositionParameters, id, cancellationToken);

            // Assert

            Assert.Equal(newName, context.Positions.Single(p => p.Id == id).Name);
        }

        [Fact]
        public async void Task_When_DeleteInTheDbAsync_Expect_PositionWasDeleteInDb()
        {
            // Arrenge

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var positionCrud = new PositionCrud(context);

            var positionParameters = new PositionParameters()
            {
                Name = "Name"
            };

            int id = await positionCrud.AddToDbAsync(positionParameters, cancellationToken);

            // Act

            await positionCrud.DeleteInTheDbAsync(id, cancellationToken);

            // Assert

            Assert.Empty(context.Positions);
        }
    }
}
