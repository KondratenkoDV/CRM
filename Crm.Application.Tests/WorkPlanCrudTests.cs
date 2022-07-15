using System;
using Xunit;
using Crm.Application.Crud.WorkPlan;

namespace Crm.Application.Tests
{
    public class WorkPlanCrudTests
    {
        [Fact]
        public async void Task_When_AddToDbAsync_Expect_WorkPlanWasAddedToDb()
        {
            // Arrenge

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var workPlanCrud = new WorkPlanCrud(context);

            var workPlanParameters = new WorkPlanParameters()
            {
                DateStart = DateTime.Now.AddDays(-10),
                DateFinish = DateTime.Now.AddDays(10),
                ContractId = 0
            };

            // Act

            await workPlanCrud.AddToDbAsync(workPlanParameters, cancellationToken);

            // Assert

            Assert.NotNull(context.WorkPlans);
        }

        [Fact]
        public async void Task_When_SelectingFromTheDbAsync_Expect_WorkPlanWasSelectFromDb()
        {
            // Arrenge

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var workPlanCrud = new WorkPlanCrud(context);

            var workPlanParameters = new WorkPlanParameters()
            {
                DateStart = DateTime.Now.AddDays(-10),
                DateFinish = DateTime.Now.AddDays(10),
                ContractId = 0
            };

            int id = await workPlanCrud.AddToDbAsync(workPlanParameters, cancellationToken);

            // Act

            var workPlan = await workPlanCrud.SelectingFromTheDbAsync(id);

            // Assert

            Assert.Equal(id, workPlan.Id);
        }

        [Fact]
        public async void Task_When_UpdateInTheDbAsync_Expect_WorkPlanWasUpdateInDb()
        {
            // Arrenge

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var workPlanCrud = new WorkPlanCrud(context);

            var workPlanParameters = new WorkPlanParameters()
            {
                DateStart = DateTime.Now.AddDays(-10),
                DateFinish = DateTime.Now.AddDays(10),
                ContractId = 0
            };

            int id = await workPlanCrud.AddToDbAsync(workPlanParameters, cancellationToken);

            var newDateStart = DateTime.Now;

            var newWorkPlanParameters = new WorkPlanParameters()
            {
                DateStart = newDateStart,
                DateFinish = DateTime.Now.AddDays(10),
                ContractId = 0
            };

            // Act

            await workPlanCrud.UpdateInTheDbAsync(newWorkPlanParameters, id, cancellationToken);

            // Assert

            Assert.Equal(newDateStart, context.WorkPlans.Single(p => p.Id == id).DateStart);
        }

        [Fact]
        public async void Task_When_DeleteInTheDbAsync_Expect_WorkPlanWasDeleteInDb()
        {
            // Arrenge

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var workPlanCrud = new WorkPlanCrud(context);

            var workPlanParameters = new WorkPlanParameters()
            {
                DateStart = DateTime.Now.AddDays(-10),
                DateFinish = DateTime.Now.AddDays(10),
                ContractId = 0
            };

            int id = await workPlanCrud.AddToDbAsync(workPlanParameters, cancellationToken);

            // Act

            await workPlanCrud.DeleteInTheDbAsync(id, cancellationToken);

            // Assert

            Assert.Empty(context.WorkPlans);
        }
    }
}
