using Crm.Domain;
using Xunit;

namespace Crm.Domain.Tests
{
    public class WorkPlanTests
    {
        [Fact]
        public void When_CreatingNewWorkPlan_Expect_Successfully()
        {
            // Arrenge

            var dateStart = DateTime.Now;

            var dateFinish = DateTime.UtcNow;

            var contractId = 0;

            // Act

            var workPlan = new WorkPlan(dateStart, dateFinish, contractId);

            // Assert

            Assert.Equal(dateStart, workPlan.DateStart);
            Assert.Equal(dateFinish, workPlan.DateFinish);
            Assert.Equal(contractId, workPlan.ContractId);
        }

        [Fact]
        public void When_ChangeDateStart_Expect_ChangeDateStartWasAddedToWorkPlan()
        {
            // Arrenge

            var dateStart = DateTime.Now;

            var dateFinish = DateTime.UtcNow;

            var contractId = 0;

            var workPlan = new WorkPlan(dateStart, dateFinish, contractId);

            var newDateStart = DateTime.UtcNow;

            // Act

            workPlan.ChangeDateStart(newDateStart);

            // Assert

            Assert.Equal(newDateStart, workPlan.DateStart);
        }

        [Fact]
        public void When_ChangeDateFinish_Expect_ChangeDateFinishWasAddedToWorkPlan()
        {
            // Arrenge

            var dateStart = DateTime.Now;

            var dateFinish = DateTime.UtcNow;

            var contractId = 0;

            var workPlan = new WorkPlan(dateStart, dateFinish, contractId);

            var newDateFinish = DateTime.Now;

            // Act

            workPlan.ChangeDateFinish(newDateFinish);

            // Assert

            Assert.Equal(newDateFinish, workPlan.DateFinish);
        }

        [Fact]
        public void When_ChangeContractId_Expect_ChangeContractIdWasAddedToWorkPlan()
        {
            // Arrenge

            var dateStart = DateTime.Now;

            var dateFinish = DateTime.UtcNow;

            var contractId = 0;

            var workPlan = new WorkPlan(dateStart, dateFinish, contractId);

            var newContractId = 1;

            // Act

            workPlan.ChangeContractId(newContractId);

            // Assert

            Assert.Equal(newContractId, workPlan.ContractId);
        }
    }
}
