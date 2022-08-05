using System;
using Xunit;
using API.Controllers;
using API.DTOs.WorkPlan;
using Moq;
using Domain.Interfaces;

namespace API.Tests.Controllers
{
    public class WorkPlanControllerTests
    {
        [Fact]
        public async void Task_When_CreateNewWorkPlan_Expect_WorkPlanWasCreated()
        {
            // Arrange

            var mock = new Mock<IWorkPlanService>();

            mock.Setup(w => w.AddAsync(
                It.IsAny<DateTime>(),
                It.IsAny<DateTime>(),
                It.IsAny<int>(),
                CancellationToken.None)).Returns(It.IsAny<Task<int>>());

            var workPlanController = new WorkPlanController(mock.Object);

            var createWorkPlanDto = new CreateWorkPlanDto()
            {
                DateStart = DateTime.Now.AddDays(-10),
                DateFinish = DateTime.Now.AddDays(2),
                ContractId = 0
            };

            // Act

            await workPlanController.CreateNewWorkPlan(createWorkPlanDto, CancellationToken.None);

            // Assert

            mock.Verify(w => w.AddAsync(
                It.IsAny<DateTime>(),
                It.IsAny<DateTime>(),
                It.IsAny<int>(),
                CancellationToken.None),
                Times.Once);
        }

        [Fact]
        public async void Task_When_SelectingWorkPlan_Expect_WorkPlanWasSelected()
        {
            // Arrange

            var mock = new Mock<IWorkPlanService>();

            mock.Setup(w => w.SelectingAsync(It.IsAny<int>()))
                .Returns(It.IsAny<Task<Domain.WorkPlan>>());

            var workPlanController = new WorkPlanController(mock.Object);

            // Act

            var workPlanValue = await workPlanController.SelectingWorkPlan(It.IsAny<int>());

            // Assert

            mock.Verify(w => w.SelectingAsync(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async void Task_When_UpdateWorkPlan_Expect_WorkPlanWasUpdate()
        {
            // Arrange

            var mock = new Mock<IWorkPlanService>();

            mock.Setup(w => w.UpdateAsync(
                It.IsAny<Domain.WorkPlan>(),
                It.IsAny<DateTime>(),
                It.IsAny<DateTime>(),
                It.IsAny<int>(),
                CancellationToken.None));

            var workPlanController = new WorkPlanController(mock.Object);

            var updateWorkPlanDto = new UpdateWorkPlanDto()
            {
                NewDateStart = DateTime.Now.AddDays(-5),
                NewDateFinish = DateTime.Now.AddDays(5),
                NewContractId = 1
            };

            // Act

            await workPlanController.UpdateWorkPlan(updateWorkPlanDto, It.IsAny<int>(), CancellationToken.None);

            // Assert

            mock.Verify(w => w.UpdateAsync(
                It.IsAny<Domain.WorkPlan>(),
                It.IsAny<DateTime>(),
                It.IsAny<DateTime>(),
                It.IsAny<int>(),
                CancellationToken.None),
                Times.Once());
        }

        [Fact]
        public async void Task_When_DeleteWorkPlan_Expect_WorkPlanWasDeleted()
        {
            // Arrange

            var mock = new Mock<IWorkPlanService>();

            mock.Setup(w => w.DeleteAsync(It.IsAny<Domain.WorkPlan>(), CancellationToken.None));

            var workPlanController = new WorkPlanController(mock.Object);

            // Act

            await workPlanController.DeleteWorkPlan(It.IsAny<int>(), CancellationToken.None);

            // Assert

            mock.Verify(w => w.DeleteAsync(
                It.IsAny<Domain.WorkPlan>(),
                CancellationToken.None),
                Times.Once());
        }
        
    }
}