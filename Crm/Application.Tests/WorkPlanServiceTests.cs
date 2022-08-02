using System;
using Xunit;
using Application.Services.WorkPlan;

namespace Application.Tests
{
    public class WorkPlanServiceTests : TestCommandBase
    {
        private (DateTime, DateTime, int) GetParameters()
        {
            var dateStart = DateTime.Now.AddDays(-10);

            var dateFinish = DateTime.Now;

            var contractId = 0;

            return (dateStart, dateFinish, contractId);
        }

        [Fact]
        public async void Task_When_AddAsync_Expect_WorkPlanWasAddedToDb()
        {
            // Arrange

            var workPlanService = new WorkPlanService(Context);

            // Act

            await workPlanService.AddAsync(
                GetParameters().Item1,
                GetParameters().Item2,
                GetParameters().Item3,
                CancellationToken.None);

            // Assert

            Assert.NotNull(Context.WorkPlans);
        }

        [Fact]
        public async void Task_When_SelectingAsync_Expect_WorkPlanWasSelectFromDb()
        {
            // Arrenge

            var workPlanService = new WorkPlanService(Context);

            int id = await workPlanService.AddAsync(
                GetParameters().Item1,
                GetParameters().Item2,
                GetParameters().Item3,
                CancellationToken.None);

            // Act

            var workPlan = await workPlanService.SelectingAsync(id);

            // Assert

            Assert.Equal(id, workPlan.Id);
        }

        [Fact]
        public async void Task_When_UpdateAsync_Expect_WorkPlanWasUpdateInDb()
        {
            // Arrenge

            var workPlanService = new WorkPlanService(Context);

            int id = await workPlanService.AddAsync(
                GetParameters().Item1,
                GetParameters().Item2,
                GetParameters().Item3,
                CancellationToken.None);

            var workPlan = await workPlanService.SelectingAsync(id);

            var newDateStart = DateTime.Now.AddDays(-5);

            var newDateFinish = DateTime.Now.AddDays(5);

            var newContractId = 1;

            // Act

            await workPlanService.UpdateAsync(
                workPlan,
                newDateStart,
                newDateFinish,
                newContractId,
                CancellationToken.None);

            // Assert

            Assert.Equal(newDateStart, Context.WorkPlans.Single(w => w.Id == id).DateStart);
            Assert.Equal(newDateFinish, Context.WorkPlans.Single(w => w.Id == id).DateFinish);
            Assert.Equal(newContractId, Context.WorkPlans.Single(w => w.Id == id).ContractId);
        }

        [Fact]
        public async void Task_When_DeleteAsync_Expect_WorkPlanWasDeleteInDb()
        {
            // Arrenge

            var workPlanService = new WorkPlanService(Context);

            int id = await workPlanService.AddAsync(
                GetParameters().Item1,
                GetParameters().Item2,
                GetParameters().Item3,
                CancellationToken.None);

            var workPlan = await workPlanService.SelectingAsync(id);

            // Act

            await workPlanService.DeleteAsync(workPlan, CancellationToken.None);

            // Assert

            Assert.Empty(Context.WorkPlans);
        }
    }
}
