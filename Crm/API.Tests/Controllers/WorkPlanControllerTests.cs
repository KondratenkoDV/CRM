using System;
using Xunit;
using API.Controllers;
using API.DTOs.WorkPlan;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Application.Services.WorkPlan;

namespace API.Tests.Controllers
{
    public class WorkPlanControllerTests : TestCommandBase
    {
        private CreateWorkPlanDto GetCreateWorkPlanDto()
        {
            return new CreateWorkPlanDto()
            {
                DateStart = DateTime.Now.AddDays(-10),
                DateFinish = DateTime.Now.AddDays(2),
                ContractId = 0
            };
        }

        [Fact]
        public async void Task_When_CreateNewWorkPlan_Expect_WorkPlanWasCreated()
        {
            // Arrange

            var mock = new Mock<WorkPlanService>(Context);

            var workPlanController = new WorkPlanController(mock.Object);

            var createWorkPlanDto = GetCreateWorkPlanDto();

            // Act

            await workPlanController.CreateNewWorkPlan(createWorkPlanDto, CancellationToken.None);

            // Assert

            Assert.NotNull(Context.WorkPlans);
        }

        [Fact]
        public async void Task_When_SelectingWorkPlan_Expect_WorkPlanWasSelected()
        {
            // Arrange

            var mock = new Mock<WorkPlanService>(Context);

            var workPlanController = new WorkPlanController(mock.Object);

            var createWorkPlanDto = GetCreateWorkPlanDto();

            var value = await workPlanController.CreateNewWorkPlan(createWorkPlanDto, CancellationToken.None);

            var result = value.Result as OkObjectResult;

            int id = (int)result.Value;

            // Act

            var workPlanValue = await workPlanController.SelectingWorkPlan(id);

            var workPlanResult = workPlanValue.Result as OkObjectResult;

            var workPlan = (SelectingWorkPlanDto)workPlanResult.Value;

            // Assert

            Assert.Equal(id, workPlan.Id);
        }

        [Fact]
        public async void Task_When_UpdateWorkPlan_Expect_WorkPlanWasUpdate()
        {
            // Arrange

            var mock = new Mock<WorkPlanService>(Context);

            var workPlanController = new WorkPlanController(mock.Object);

            var createWorkPlanDto = GetCreateWorkPlanDto();

            var updateWorkPlanDto = new UpdateWorkPlanDto()
            {
                NewDateStart = DateTime.Now.AddDays(-5),
                NewDateFinish = DateTime.Now.AddDays(5),
                NewContractId = 1
            };

            var value = await workPlanController.CreateNewWorkPlan(createWorkPlanDto, CancellationToken.None);

            var result = value.Result as OkObjectResult;

            int id = (int)result.Value;

            // Act

            await workPlanController.UpdateWorkPlan(updateWorkPlanDto, id, CancellationToken.None);

            // Assert

            Assert.Equal(updateWorkPlanDto.NewDateStart, Context.WorkPlans.Single(w => w.Id == id).DateStart);
            Assert.Equal(updateWorkPlanDto.NewDateFinish, Context.WorkPlans.Single(w => w.Id == id).DateFinish);
            Assert.Equal(updateWorkPlanDto.NewContractId, Context.WorkPlans.Single(w => w.Id == id).ContractId);
        }

        [Fact]
        public async void Task_When_DeleteWorkPlan_Expect_WorkPlanWasDeleted()
        {
            // Arrange

            var mock = new Mock<WorkPlanService>(Context);

            var workPlanController = new WorkPlanController(mock.Object);

            var createWorkPlanDto = GetCreateWorkPlanDto();

            var value = await workPlanController.CreateNewWorkPlan(createWorkPlanDto, CancellationToken.None);

            var result = value.Result as OkObjectResult;

            int id = (int)result.Value;

            // Act

            await workPlanController.DeleteWorkPlan(id, CancellationToken.None);

            // Assert

            Assert.Empty(Context.WorkPlans);
        }
        
    }
}