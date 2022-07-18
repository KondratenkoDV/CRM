using System;
using Xunit;
using Application.Tests.Connection;
using Application.Services.WorkPlan;

namespace Application.Tests
{
    public class WorkPlanServiceTests
    {
        [Fact]
        public async void Task_When_AddAsync_Expect_WorkPlanWasAddedToDb()
        {
            // Arrenge

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var workPlanService = new WorkPlanService(context);

            var dateStart = DateTime.Now.AddDays(-10);

            var dateFinish = DateTime.Now;

            var contractId = 0;

            // Act

            await workPlanService.AddAsync(
                dateStart,
                dateFinish,
                contractId,
                cancellationToken);

            // Assert

            Assert.NotNull(context.WorkPlans);
        }

        [Fact]
        public async void Task_When_SelectingAsync_Expect_WorkPlanWasSelectFromDb()
        {
            // Arrenge

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var workPlanService = new WorkPlanService(context);

            var dateStart = DateTime.Now.AddDays(-10);

            var dateFinish = DateTime.Now;

            var contractId = 0;

            int id = await workPlanService.AddAsync(
                dateStart,
                dateFinish,
                contractId,
                cancellationToken);

            // Act

            var workPlan = await workPlanService.SelectingAsync(id);

            // Assert

            Assert.Equal(id, workPlan.Id);
        }

        [Fact]
        public async void Task_When_UpdateAsync_Expect_WorkPlanWasUpdateInDb()
        {
            // Arrenge

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var workPlanService = new WorkPlanService(context);

            var dateStart = DateTime.Now.AddDays(-10);

            var dateFinish = DateTime.Now;

            var contractId = 0;

            int id = await workPlanService.AddAsync(
                dateStart,
                dateFinish,
                contractId,
                cancellationToken);

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
                cancellationToken);

            // Assert

            Assert.Equal(newDateStart, context.WorkPlans.Single(w => w.Id == id).DateStart);
            Assert.Equal(newDateFinish, context.WorkPlans.Single(w => w.Id == id).DateFinish);
            Assert.Equal(newContractId, context.WorkPlans.Single(w => w.Id == id).ContractId);
        }

        [Fact]
        public async void Task_When_DeleteAsync_Expect_WorkPlanWasDeleteInDb()
        {
            // Arrenge

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var workPlanService = new WorkPlanService(context);

            var dateStart = DateTime.Now.AddDays(-10);

            var dateFinish = DateTime.Now;

            var contractId = 0;

            int id = await workPlanService.AddAsync(
                dateStart,
                dateFinish,
                contractId,
                cancellationToken);

            var workPlan = await workPlanService.SelectingAsync(id);

            // Act

            await workPlanService.DeleteAsync(workPlan, cancellationToken);

            // Assert

            Assert.Empty(context.WorkPlans);
        }
    }
}
