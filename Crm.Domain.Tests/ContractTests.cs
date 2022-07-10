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

            var price = 0;

            var clientId = 0;

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

            var name = "name";

            var surname = "surname";

            var positionId = 0;

            var employee = new Employee(name, surname, positionId);

            var subject = "subject";

            var address = "address";

            var price = 0;

            var clientId = 0;

            var contract = new Contract(subject, address, price, clientId);

            // Act

            contract.AddEmployee(employee);

            // Assert

            Assert.NotEmpty(contract.Employees);
        }

        [Fact]
        public void When_AddWorkPlan_Expect_PlanWasAddedToCollection()
        {
            // Arrenge

            var dateStart = DateTime.Now;

            var dateFinish = DateTime.UtcNow;

            var contractId = 0;

            var workPlan = new WorkPlan(dateStart, dateFinish, contractId);

            var subject = "subject";

            var address = "address";

            var price = 0;

            var clientId = 0;

            var contract = new Contract(subject, address, price, clientId);

            // Act

            contract.AddWorkPlan(workPlan);

            // Assert

            Assert.NotEmpty(contract.WorkPlans);
        }
    }
}
