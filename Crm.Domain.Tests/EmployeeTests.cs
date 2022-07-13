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

            var firstName = "firstName";

            var lastName = "lastName";

            var positionId = 0;

            // Act

            var employee = new Employee(firstName, lastName, positionId);

            // Assert

            Assert.Equal(positionId, employee.PositionId);
            Assert.Equal(firstName, employee.FirstName);
            Assert.Equal(lastName, employee.LastName);
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

            var firstName = "firstName";

            var lastName = "lastName";

            var positionId = 0;

            var employee = new Employee(firstName, lastName, positionId);

            // Act

            employee.AddContract(contract);

            // Assert

            Assert.NotEmpty(employee.Contracts);
        }

        [Fact]
        public void When_AddName_Expect_NameWasAddedToEmployee()
        {
            // Arrenge

            var firstName = "firstName";

            var lastName = "lastName";

            var positionId = 0;

            var employee = new Employee(firstName, lastName, positionId);

            var expected = "lastName firstName";

            // Act

            employee.AddName();

            //Assert

            Assert.Equal(expected, employee.Name);
        }

        [Fact]
        public void When_ChangeName_Expect_ChangeNameWasAddedToEmployee()
        {
            // Arrenge

            var firstName = "firstName";

            var lastName = "lastName";

            var positionId = 0;

            var employee = new Employee(firstName, lastName, positionId);

            var newFirstName = "newFirstName";

            var newLastName = "newLastName";

            var expected = "newLastName newFirstName";

            // Act

            employee.ChangeName(newLastName, newFirstName);

            //Assert

            Assert.Equal(expected, employee.Name);
        }

        [Fact]
        public void When_ChangePositionId_Expect_ChangePositionIdWasAddedToEmployee()
        {
            // Arrenge

            var firstName = "firstName";

            var lastName = "lastName";

            var positionId = 0;

            var employee = new Employee(firstName, lastName, positionId);

            var newPositionId = 1;

            // Act

            employee.ChangePositionId(newPositionId);

            //Assert

            Assert.Equal(newPositionId, employee.PositionId);
        }
    }
}
