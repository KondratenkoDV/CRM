using Crm.Domain;
using Xunit;

namespace Crm.Domain.Tests
{
    public class EmployeeTests
    {
        [Fact]
        public void When_CreatingNewEmployee_Expect_Successfully()
        {
            // Arrenge

            var name = "name";

            var surname = "surname";

            var positionId = 0;

            var expected = "surname name";

            // Act

            var employee = new Employee(name, surname, positionId);

            // Assert

            Assert.Equal(positionId, employee.PositionId);
            Assert.Equal(expected, employee.Name);
        }

        [Fact]
        public void When_AddContract_Expect_ContractWasAddedToCollection()
        {
            // Arrenge

            var subject = "subject";

            var address = "address";

            var price = 0;

            var clientId = 0;

            var contract = new Contract(subject, address, price, clientId);

            var name = "name";

            var surname = "surname";

            var positionId = 0;

            var employee = new Employee(name, surname, positionId);

            // Act

            employee.AddContract(contract);

            // Assert

            Assert.NotEmpty(employee.Contracts);
        }
    }
}
