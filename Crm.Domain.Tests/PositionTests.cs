using Crm.Domain;
using Xunit;

namespace Crm.Domain.Tests
{
    public class PositionTests
    {
        [Fact]
        public void When_CreatingNewPosition_Expect_Successfully()
        {
            // Arrenge

            var name = "test";

            // Act

            var position = new Position(name);

            // Assert

            Assert.Equal(name, position.Name);
        }

        [Fact]
        public void When_AddEmployee_Expect_EmployeeWasAddedToCollection()
        {
            // Arrenge

            var employee = new Employee(1);

            // Act

            var position = new Position(null!);

            position.AddEmployee(employee);

            // Assert

            Assert.NotEmpty(position.Employees);
        }
    }
}
