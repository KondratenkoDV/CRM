using System;
using Xunit;
using API.Tests.Connection;
using API.Controllers;
using API.DTOs.WorkPlan;
using Microsoft.AspNetCore.Mvc;

namespace API.Tests.Controllers
{
    public class WorkPlanControllerTests
    {
        [Fact]
        public async void Task_When_CreateNewWorkPlan_Expect_CreateNewWorkPlanWasAddedToDb()
        {
            // Arrange

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var workPlanController = new WorkPlanController(context);

            var createWorkPlanDto = new CreateWorkPlanDto()
            {
                DateStart = DateTime.Now.AddDays(-10),
                DateFinish = DateTime.Now.AddDays(2),
                ContractId = 0
            };

            // Act

            await workPlanController.CreateNewWorkPlan(createWorkPlanDto, cancellationToken);

            // Assert

            Assert.NotNull(context.WorkPlans);
        }

        [Fact]
        public async void Task_When_SelectingWorkPlan_Expect_SelectingWorkPlanWasAddedFromDb()
        {
            // Arrange

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var workPlanController = new WorkPlanController(context);

            var createWorkPlanDto = new CreateWorkPlanDto()
            {
                DateStart = DateTime.Now.AddDays(-10),
                DateFinish = DateTime.Now.AddDays(2),
                ContractId = 0
            };

            var value = await workPlanController.CreateNewWorkPlan(createWorkPlanDto, cancellationToken);

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
        public async void Task_When_UpdateWorkPlan_Expect_UpdateWorkPlanWasAddedInDb()
        {
            // Arrange

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var workPlanController = new WorkPlanController(context);

            var createWorkPlanDto = new CreateWorkPlanDto()
            {
                DateStart = DateTime.Now.AddDays(-10),
                DateFinish = DateTime.Now.AddDays(2),
                ContractId = 0
            };

            var updateWorkPlanDto = new UpdateWorkPlanDto()
            {
                NewDateStart = DateTime.Now.AddDays(-5),
                NewDateFinish = DateTime.Now.AddDays(5),
                NewContractId = 1
            };

            var value = await workPlanController.CreateNewWorkPlan(createWorkPlanDto, cancellationToken);

            var result = value.Result as OkObjectResult;

            int id = (int)result.Value;

            // Act

            await workPlanController.UpdateWorkPlan(updateWorkPlanDto, id, cancellationToken);

            // Assert

            Assert.Equal(updateWorkPlanDto.NewDateStart, context.WorkPlans.Single(w => w.Id == id).DateStart);
            Assert.Equal(updateWorkPlanDto.NewDateFinish, context.WorkPlans.Single(w => w.Id == id).DateFinish);
            Assert.Equal(updateWorkPlanDto.NewContractId, context.WorkPlans.Single(w => w.Id == id).ContractId);
        }

        [Fact]
        public async void Task_When_DeleteWorkPlan_Expect_DeleteWorkPlanWasAddedFromDb()
        {
            // Arrange

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var workPlanController = new WorkPlanController(context);

            var createWorkPlanDto = new CreateWorkPlanDto()
            {
                DateStart = DateTime.Now.AddDays(-10),
                DateFinish = DateTime.Now.AddDays(2),
                ContractId = 0
            };

            var value = await workPlanController.CreateNewWorkPlan(createWorkPlanDto, cancellationToken);

            var result = value.Result as OkObjectResult;

            int id = (int)result.Value;

            // Act

            await workPlanController.DeleteWorkPlan(id, cancellationToken);

            // Assert

            Assert.Empty(context.WorkPlans);
        }
        
    }
}
