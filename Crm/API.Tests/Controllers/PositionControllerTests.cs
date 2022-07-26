using System;
using Xunit;
using API.Tests.Connection;
using API.Controllers;
using API.DTOs.Position;
using Microsoft.AspNetCore.Mvc;

namespace API.Tests.Controllers
{
    public class PositionControllerTests
    {
        [Fact]
        public async void Task_When_CreateNewPosition_Expect_PositionWasCreated()
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
        public async void Task_When_SelectingPosition_Expect_PositionWasSelected()
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

            var value = await positionController.CreateNewPosition(createPositionDto, cancellationToken);

            var result = value.Result as OkObjectResult;

            int id = (int)result.Value;

            // Act

            var positionValue = await positionController.SelectingPosition(id);

            var positionResult = positionValue.Result as OkObjectResult;

            var position = (SelectingPositionDto)positionResult.Value;

            // Assert

            Assert.Equal(id, position.Id);
        }

        [Fact]
        public async void Task_When_UpdatePosition_Expect_PositionWasUpdate()
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

            var value = await positionController.CreateNewPosition(createPositionDto, cancellationToken);

            var result = value.Result as OkObjectResult;

            int id = (int)result.Value;

            // Act

            await positionController.UpdatePosition(updatePositionDto, id, cancellationToken);

            // Assert

            Assert.Equal(updatePositionDto.NewName, context.Positions.Single(p => p.Id == id).Name);
        }

        [Fact]
        public async void Task_When_DeletePosition_Expect_PositionWasDeleted()
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

            var value = await positionController.CreateNewPosition(createPositionDto, cancellationToken);

            var result = value.Result as OkObjectResult;

            int id = (int)result.Value;

            // Act

            await positionController.DeletePosition(id, cancellationToken);

            // Assert

            Assert.Empty(context.Positions);
        }
        
    }
}
