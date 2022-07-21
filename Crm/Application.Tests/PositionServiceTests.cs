using System;
using Xunit;
using Application.Tests.Connection;
using Application.Services.Position;

namespace Application.Tests
{
    public class PositionServiceTests
    {
        [Fact]
        public async void Task_When_AddAsync_Expect_PositionWasAddedToDb()
        {
            // Arrange

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var positionService = new PositionService(context);

            var name = "name";

            // Act

            await positionService.AddAsync(name, cancellationToken);

            // Assert

            Assert.NotNull(context.Positions);
        }

        [Fact]
        public async void Task_When_SelectingAsync_Expect_PositionWasSelectFromDb()
        {
            // Arrenge

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var positionService = new PositionService(context);

            var name = "name";

            int id = await positionService.AddAsync(name, cancellationToken);

            // Act

            var position = await positionService.SelectingAsync(id);

            // Assert

            Assert.Equal(id, position.Id);
        }

        [Fact]
        public async void Task_When_UpdateAsync_Expect_PositionWasUpdateInDb()
        {
            // Arrenge

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var positionService = new PositionService(context);

            var name = "name";

            int id = await positionService.AddAsync(name, cancellationToken);

            var position = await positionService.SelectingAsync(id);

            var newName = "newName";

            // Act

            await positionService.UpdateAsync(position, newName, cancellationToken);

            // Assert

            Assert.Equal(newName, context.Positions.Single(p => p.Id == id).Name);
        }

        [Fact]
        public async void Task_When_DeleteAsync_Expect_PositionWasDeleteInDb()
        {
            // Arrenge

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var positionService = new PositionService(context);

            var name = "name";

            int id = await positionService.AddAsync(name, cancellationToken);

            var position = await positionService.SelectingAsync(id);

            // Act

            await positionService.DeleteAsync(position, cancellationToken);

            // Assert

            Assert.Empty(context.Positions);
        }
    }
}
