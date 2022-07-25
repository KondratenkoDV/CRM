using System;
using Xunit;
using API.Tests.Connection;
using API.Controllers;
using API.DTOs.Position;

namespace API.Tests.Controllers
{
    public class PositionControllerTests
    {
        [Fact]
        public async void Task_When_CreateNewPosition_Expect_CreateNewPositionWasAddedToDb()
        {
            // Arrange

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var positionController = new PositionController(context);

            var createPositionDto = new CreatePositionDto()
            {
                Name = "Name"
            };

            // Act

            await positionController.CreateNewPosition(createPositionDto, cancellationToken);

            // Assert

            Assert.NotNull(context.Positions);
        }

        [Fact]
        public async void Task_When_SelectingPosition_Expect_SelectingPositionWasAddedFromDb()
        {
            // Arrange

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var positionController = new PositionController(context);

            var createPositionDto = new CreatePositionDto()
            {
                Name = "Name"
            };

            var id = await positionController.CreateNewPosition(createPositionDto, cancellationToken);

            // Act

            var position = await positionController.SelectingPosition(id.Value);

            // Assert

            Assert.Equal(id, position.Value.Id);
        }

        [Fact]
        public async void Task_When_UpdatePosition_Expect_UpdatePositionWasAddedInDb()
        {
            // Arrange

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var positionController = new PositionController(context);

            var createPositionDto = new CreatePositionDto()
            {
                Name = "Name"
            };

            var updatePositionDto = new UpdatePositionDto()
            {
                NewName = "NewName"
            };

            var id = await positionController.CreateNewPosition(createPositionDto, cancellationToken);

            // Act

            await positionController.UpdatePosition(updatePositionDto, id.Value, cancellationToken);

            // Assert

            Assert.Equal(updatePositionDto.NewName, context.Positions.Single(p => p.Id == id.Value).Name);
        }

        [Fact]
        public async void Task_When_DeletePosition_Expect_DeletePositionWasAddedFromDb()
        {
            // Arrange

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var positionController = new PositionController(context);

            var createPositionDto = new CreatePositionDto()
            {
                Name = "Name"
            };

            var id = await positionController.CreateNewPosition(createPositionDto, cancellationToken);

            // Act

            await positionController.DeletePosition(id.Value, cancellationToken);

            // Assert

            Assert.Empty(context.Positions);
        }
        
    }
}
