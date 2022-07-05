using Crm.Domain;
using Xunit;

namespace Crm.Domain.Tests
{
    public class ContractTests
    {
        [Fact]
        public void When_CreatingNewContract_Expect_Successfully()
        {
            // Arrenge

            var subject = "subject";

            var address = "address";

            var price = 0m;

            var clientId = 1;

            // Act

            var contract = new Contract(subject, address, price, clientId);

            //Assert

            Assert.Equal(subject, contract.Subject);
            Assert.Equal(address, contract.Address);
            Assert.Equal(price, contract.Price);
            Assert.Equal(clientId, contract.ClientId);
        }

        [Fact]
        public void When_AddEmployee_Expect_EmployeeWasAddedToCollection()
        {
            // Arrenge

            var employee = new Employee(1);

            // Act

            var contract = new Contract(null!, null!, 0, 1);

            contract.AddEmployee(employee);

            // Assert

            Assert.NotEmpty(contract.Employees);
        }

        [Fact]
        public void When_AddWorkPlan_Expect_PlanWasAddedToCollection()
        {
            // Arrenge

            var workPlan = new WorkPlan(DateTime.Now, DateTime.Now, 1);

            // Act

            var contract = new Contract(null!, null!, 0, 1);

            contract.AddWorkPlan(workPlan);

            // Assert

            Assert.NotEmpty(contract.WorkPlans);
        }
    }
}
