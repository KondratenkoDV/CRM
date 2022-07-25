using System;
using Xunit;
using API.Tests.Connection;
using API.Controllers;
using API.DTOs.WorkPlan;

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

            var id = await workPlanController.CreateNewWorkPlan(createWorkPlanDto, cancellationToken);

            // Act

            var workPlan = await workPlanController.SelectingWorkPlan(id.Value);

            // Assert

            Assert.Equal(id, workPlan.Value.Id);
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

            var id = await workPlanController.CreateNewWorkPlan(createWorkPlanDto, cancellationToken);

            // Act

            await workPlanController.UpdateWorkPlan(updateWorkPlanDto, id.Value, cancellationToken);

            // Assert

            Assert.Equal(updateWorkPlanDto.NewDateStart, context.WorkPlans.Single(w => w.Id == id.Value).DateStart);
            Assert.Equal(updateWorkPlanDto.NewDateFinish, context.WorkPlans.Single(w => w.Id == id.Value).DateFinish);
            Assert.Equal(updateWorkPlanDto.NewContractId, context.WorkPlans.Single(w => w.Id == id.Value).ContractId);
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

            var id = await workPlanController.CreateNewWorkPlan(createWorkPlanDto, cancellationToken);

            // Act

            await workPlanController.DeleteWorkPlan(id.Value, cancellationToken);

            // Assert

            Assert.Empty(context.WorkPlans);
        }
        
    }
}
