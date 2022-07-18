using System;
using Xunit;

namespace Domain.Tests
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

            var firstName = "firstName";

            var lastName = "surname";

            var positionId = 0;

            var employee = new Employee(firstName, lastName, positionId);

            var namePosition = "name";

            var position = new Position(namePosition);

            // Act

            position.AddEmployee(employee);

            // Assert

            Assert.NotEmpty(position.Employees);
        }

        [Fact]
        public void When_ChangeName_Expect_ChangeNameWasAddedToPosition()
        {
            // Arrenge

            var name = "name";

            var position = new Position(name);

            var newName = "newName";

            // Act

            position.ChangeName(newName);

            // Assert

            Assert.Equal(newName, position.Name);
        }
    }
}
