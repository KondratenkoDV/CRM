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
        private int CreateWorkPlan(Mock<IWorkPlanService> mock)
        {
            return mock.Object.AddAsync(
                DateTime.Now.AddDays(-10),
                DateTime.Now.AddDays(2),
                0,
                CancellationToken.None).Result;
        }

        [Fact]
        public async void Task_When_CreateNewWorkPlan_Expect_WorkPlanWasCreated()
        {
            // Arrange

            var createWorkPlanDto = new CreateWorkPlanDto()
            {
                DateStart = DateTime.Now.AddDays(-10),
                DateFinish = DateTime.Now.AddDays(2),
                ContractId = 0
            };

            var mock = new Mock<IWorkPlanService>();

            mock.Setup(w => w.AddAsync(
                createWorkPlanDto.DateStart,
                createWorkPlanDto.DateFinish,
                createWorkPlanDto.ContractId,
                CancellationToken.None)).Returns(It.IsAny<Task<int>>());

            var workPlanController = new WorkPlanController(mock.Object);

            // Act

            await workPlanController.CreateNewWorkPlan(createWorkPlanDto, CancellationToken.None);

            // Assert

            mock.Verify(w => w.AddAsync(
                createWorkPlanDto.DateStart,
                createWorkPlanDto.DateFinish,
                createWorkPlanDto.ContractId,
                CancellationToken.None),
                Times.Once);
        }

        [Fact]
        public async void Task_When_SelectingWorkPlan_Expect_WorkPlanWasSelected()
        {
            // Arrange

            var mock = new Mock<IWorkPlanService>();

            var id = CreateWorkPlan(mock);

            mock.Setup(w => w.SelectingAsync(id))
                .Returns(It.IsAny<Task<Domain.WorkPlan>>());

            var workPlanController = new WorkPlanController(mock.Object);

            // Act

            var workPlanValue = await workPlanController.SelectingWorkPlan(id);

            // Assert

            mock.Verify(w => w.SelectingAsync(id), Times.Once());
        }

        [Fact]
        public async void Task_When_UpdateWorkPlan_Expect_WorkPlanWasUpdate()
        {
            // Arrange

            var updateWorkPlanDto = new UpdateWorkPlanDto()
            {
                NewDateStart = DateTime.Now.AddDays(-5),
                NewDateFinish = DateTime.Now.AddDays(5),
                NewContractId = 1
            };

            var mock = new Mock<IWorkPlanService>();

            var id = CreateWorkPlan(mock);

            var workPlan = mock.Object.SelectingAsync(id).Result;

            mock.Setup(w => w.UpdateAsync(
                workPlan,
                updateWorkPlanDto.NewDateStart,
                updateWorkPlanDto.NewDateFinish,
                updateWorkPlanDto.NewContractId,
                CancellationToken.None));

            var workPlanController = new WorkPlanController(mock.Object);

            // Act

            await workPlanController.UpdateWorkPlan(updateWorkPlanDto, id, CancellationToken.None);

            // Assert

            mock.Verify(w => w.UpdateAsync(
                workPlan,
                updateWorkPlanDto.NewDateStart,
                updateWorkPlanDto.NewDateFinish,
                updateWorkPlanDto.NewContractId,
                CancellationToken.None),
                Times.Once());
        }

        [Fact]
        public async void Task_When_DeleteWorkPlan_Expect_WorkPlanWasDeleted()
        {
            // Arrange

            var mock = new Mock<IWorkPlanService>();

            var id = CreateWorkPlan(mock);

            var workPlan = mock.Object.SelectingAsync(id).Result;

            mock.Setup(w => w.DeleteAsync(workPlan, CancellationToken.None));

            var workPlanController = new WorkPlanController(mock.Object);

            // Act

            await workPlanController.DeleteWorkPlan(id, CancellationToken.None);

            // Assert

            mock.Verify(w => w.DeleteAsync(
                workPlan,
                CancellationToken.None),
                Times.Once());
        }        
    }
}