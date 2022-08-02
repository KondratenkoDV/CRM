using System;
using Xunit;
using API.Controllers;
using API.DTOs.Position;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Application.Services.Position;

namespace API.Tests.Controllers
{
    public class PositionControllerTests : TestCommandBase
    {
        private CreatePositionDto GetCreatePositionDto()
        {
            return new CreatePositionDto()
            {
                Name = "Name"
            };
        }

        [Fact]
        public async void Task_When_CreateNewPosition_Expect_PositionWasCreated()
        {
            // Arrange

            var mock = new Mock<PositionService>(Context);

            var positionController = new PositionController(mock.Object);

            var createPositionDto = GetCreatePositionDto();

            // Act

            await positionController.CreateNewPosition(createPositionDto, CancellationToken.None);

            // Assert

            Assert.NotNull(Context.Positions);
        }

        [Fact]
        public async void Task_When_SelectingPosition_Expect_PositionWasSelected()
        {
            // Arrange

            var mock = new Mock<PositionService>(Context);

            var positionController = new PositionController(mock.Object);

            var createPositionDto = GetCreatePositionDto();

            var value = await positionController.CreateNewPosition(createPositionDto, CancellationToken.None);

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

            var mock = new Mock<PositionService>(Context);

            var positionController = new PositionController(mock.Object);

            var createPositionDto = GetCreatePositionDto();

            var updatePositionDto = new UpdatePositionDto()
            {
                NewName = "NewName"
            };

            var value = await positionController.CreateNewPosition(createPositionDto, CancellationToken.None);

            var result = value.Result as OkObjectResult;

            int id = (int)result.Value;

            // Act

            await positionController.UpdatePosition(updatePositionDto, id, CancellationToken.None);

            // Assert

            Assert.Equal(updatePositionDto.NewName, Context.Positions.Single(p => p.Id == id).Name);
        }

        [Fact]
        public async void Task_When_DeletePosition_Expect_PositionWasDeleted()
        {
            // Arrange

            var mock = new Mock<PositionService>(Context);

            var positionController = new PositionController(mock.Object);

            var createPositionDto = GetCreatePositionDto();

            var value = await positionController.CreateNewPosition(createPositionDto, CancellationToken.None);

            var result = value.Result as OkObjectResult;

            int id = (int)result.Value;

            // Act

            await positionController.DeletePosition(id, CancellationToken.None);

            // Assert

            Assert.Empty(Context.Positions);
        }
        
    }
}
