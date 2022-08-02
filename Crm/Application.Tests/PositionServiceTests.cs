using System;
using Xunit;
using Application.Services.Position;

namespace Application.Tests
{
    public class PositionServiceTests : TestCommandBase
    {
        [Fact]
        public async void Task_When_AddAsync_Expect_PositionWasAddedToDb()
        {
            // Arrange

            var positionService = new PositionService(Context);

            var name = "name";

            // Act

            await positionService.AddAsync(name, CancellationToken.None);

            // Assert

            Assert.NotNull(Context.Positions);
        }

        [Fact]
        public async void Task_When_SelectingAsync_Expect_PositionWasSelectFromDb()
        {
            // Arrenge

            var positionService = new PositionService(Context);

            var name = "name";

            int id = await positionService.AddAsync(name, CancellationToken.None);

            // Act

            var position = await positionService.SelectingAsync(id);

            // Assert

            Assert.Equal(id, position.Id);
        }

        [Fact]
        public async void Task_When_UpdateAsync_Expect_PositionWasUpdateInDb()
        {
            // Arrenge

            var positionService = new PositionService(Context);

            var name = "name";

            int id = await positionService.AddAsync(name, CancellationToken.None);

            var position = await positionService.SelectingAsync(id);

            var newName = "newName";

            // Act

            await positionService.UpdateAsync(position, newName, CancellationToken.None);

            // Assert

            Assert.Equal(newName, Context.Positions.Single(p => p.Id == id).Name);
        }

        [Fact]
        public async void Task_When_DeleteAsync_Expect_PositionWasDeleteInDb()
        {
            // Arrenge

            var positionService = new PositionService(Context);

            var name = "name";

            int id = await positionService.AddAsync(name, CancellationToken.None);

            var position = await positionService.SelectingAsync(id);

            // Act

            await positionService.DeleteAsync(position, CancellationToken.None);

            // Assert

            Assert.Empty(Context.Positions);
        }
    }
}
