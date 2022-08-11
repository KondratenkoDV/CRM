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
        private int CreatePosition(Mock<IPositionService> mock)
        {
            return mock.Object.AddAsync("Name", CancellationToken.None).Result;
        }

        [Fact]
        public async void Task_When_CreateNewPosition_Expect_PositionWasCreated()
        {
            // Arrange

            var createPositionDto = new CreatePositionDto()
            {
                Name = "Name"
            };

            var mock = new Mock<IPositionService>();

            mock.Setup(p => p.AddAsync(createPositionDto.Name, CancellationToken.None))
                .Returns(It.IsAny<Task<int>>());

            var positionController = new PositionController(mock.Object);
            
            // Act

            await positionController.CreateNewPosition(createPositionDto, CancellationToken.None);

            // Assert

            mock.Verify(p => p.AddAsync(
                createPositionDto.Name,
                CancellationToken.None),
                Times.Once());
        }

        [Fact]
        public async void Task_When_SelectingPosition_Expect_PositionWasSelected()
        {
            // Arrange

            var mock = new Mock<IPositionService>();

            var id = CreatePosition(mock);

            mock.Setup(p => p.SelectingAsync(id))
                .Returns(It.IsAny<Task<Domain.Position>>());

            var positionController = new PositionController(mock.Object);

            // Act

            await positionController.SelectingPosition(id);

            // Assert

            mock.Verify(p => p.SelectingAsync(id), Times.Once());
        }

        [Fact]
        public async void Task_When_UpdatePosition_Expect_PositionWasUpdate()
        {
            // Arrange

            var updatePositionDto = new UpdatePositionDto()
            {
                NewName = "NewName"
            };

            var mock = new Mock<IPositionService>();

            var id = CreatePosition(mock);

            var position = mock.Object.SelectingAsync(id).Result;

            mock.Setup(p => p.UpdateAsync(
                position,
                updatePositionDto.NewName,
                CancellationToken.None));

            var positionController = new PositionController(mock.Object);

            // Act

            await positionController.UpdatePosition(updatePositionDto, id, CancellationToken.None);

            // Assert

            mock.Verify(p => p.UpdateAsync(
                position,
                updatePositionDto.NewName,
                CancellationToken.None),
                Times.Once());
        }

        [Fact]
        public async void Task_When_DeletePosition_Expect_PositionWasDeleted()
        {
            // Arrange

            var mock = new Mock<IPositionService>();

            var id = CreatePosition(mock);

            var position = mock.Object.SelectingAsync(id).Result;

            mock.Setup(p => p.DeleteAsync(position, CancellationToken.None));

            var positionController = new PositionController(mock.Object);

            // Act

            await positionController.DeletePosition(id, CancellationToken.None);

            // Assert

            mock.Verify(p => p.DeleteAsync(
                position,
                CancellationToken.None),
                Times.Once());
        }
        
    }
}
