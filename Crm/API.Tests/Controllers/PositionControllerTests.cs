using System;
using Xunit;
using API.Controllers;
using API.DTOs.Position;
using Moq;
using Domain.Interfaces;

namespace API.Tests.Controllers
{
    public class PositionControllerTests
    {
        [Fact]
        public async void Task_When_CreateNewPosition_Expect_PositionWasCreated()
        {
            // Arrange

            var mock = new Mock<IPositionService>();

            mock.Setup(p => p.AddAsync(It.IsAny<string>(), CancellationToken.None))
                .Returns(It.IsAny<Task<int>>());

            var positionController = new PositionController(mock.Object);

            var createPositionDto = new CreatePositionDto()
            {
                Name = "Name"
            };

            // Act

            await positionController.CreateNewPosition(createPositionDto, CancellationToken.None);

            // Assert

            mock.Verify(p => p.AddAsync(
                It.IsAny<string>(),
                CancellationToken.None),
                Times.Once());
        }

        [Fact]
        public async void Task_When_SelectingPosition_Expect_PositionWasSelected()
        {
            // Arrange

            var mock = new Mock<IPositionService>();

            mock.Setup(p => p.SelectingAsync(It.IsAny<int>()))
                .Returns(It.IsAny<Task<Domain.Position>>());

            var positionController = new PositionController(mock.Object);

            // Act

            await positionController.SelectingPosition(It.IsAny<int>());

            // Assert

            mock.Verify(p => p.SelectingAsync(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async void Task_When_UpdatePosition_Expect_PositionWasUpdate()
        {
            // Arrange

            var mock = new Mock<IPositionService>();

            mock.Setup(p => p.UpdateAsync(
                It.IsAny<Domain.Position>(),
                It.IsAny<string>(),
                CancellationToken.None));

            var positionController = new PositionController(mock.Object);

            var updatePositionDto = new UpdatePositionDto()
            {
                NewName = "NewName"
            };

            // Act

            await positionController.UpdatePosition(updatePositionDto, It.IsAny<int>(), CancellationToken.None);

            // Assert

            mock.Verify(p => p.UpdateAsync(
                It.IsAny<Domain.Position>(),
                It.IsAny<string>(),
                CancellationToken.None),
                Times.Once());
        }

        [Fact]
        public async void Task_When_DeletePosition_Expect_PositionWasDeleted()
        {
            // Arrange

            var mock = new Mock<IPositionService>();

            mock.Setup(p => p.DeleteAsync(It.IsAny<Domain.Position>(), CancellationToken.None));

            var positionController = new PositionController(mock.Object);

            // Act

            await positionController.DeletePosition(It.IsAny<int>(), CancellationToken.None);

            // Assert

            mock.Verify(p => p.DeleteAsync(
                It.IsAny<Domain.Position>(),
                CancellationToken.None),
                Times.Once());
        }
        
    }
}
