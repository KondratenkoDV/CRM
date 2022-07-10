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

            var name = "name";

            // Act

            var position = new Position(name);

            // Assert

            Assert.Equal(name, position.Name);
        }

        [Fact]
        public void When_AddEmployee_Expect_EmployeeWasAddedToCollection()
        {
            // Arrenge

            var name = "name";

            var surname = "surname";

            var positionId = 0;

            var employee = new Employee(name, surname, positionId);

            var namePosition = "name";

            var position = new Position(namePosition);

            // Act

            position.AddEmployee(employee);

            // Assert

            Assert.NotEmpty(position.Employees);
        }
    }
}
