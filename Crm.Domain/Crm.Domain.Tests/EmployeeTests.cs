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

            var positionId = 1;

            // Act

            var employee = new Employee(positionId);

            // Assert

            Assert.Equal(positionId, employee.PositionId);
        }

        [Fact]
        public void When_AddName_Expect_NameWasAddedToEmployee()
        {
            // Arrenge

            var name = "test";

            var surname = "test2";

            var expected = $"{surname} {name}";

            // Act

            var employee = new Employee(1);

            employee.AddName(name, surname);

            // Assert

            Assert.Equal(expected, employee.Name);
        }

        [Fact]
        public void When_AddContract_Expect_ContractWasAddedToCollection()
        {
            // Arrenge

            var contract = new Contract("test", null!, 0, 1);

            // Act

            var employee = new Employee(1);

            employee.AddContract(contract);

            // Assert

            Assert.NotEmpty(employee.Contracts);
        }
    }
}
