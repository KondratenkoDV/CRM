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

            var contractId = 1;

            // Act

            var workPlan = new WorkPlan(dateStart, dateFinish, contractId);

            // Assert

            Assert.Equal(dateStart, workPlan.DateStart);

            Assert.Equal(dateFinish, workPlan.DateFinish);

            Assert.Equal(contractId, workPlan.ContractId);
        }
    }
}
