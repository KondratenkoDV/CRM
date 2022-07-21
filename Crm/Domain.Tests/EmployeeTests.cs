using System;
using Xunit;

namespace Domain.Tests
{
    public class EmployeeTests
    {
        [Fact]
        public void When_CreatingNewEmployee_Expect_Successfully()
        {
            // Arrange

            var firstName = "firstName";

            var lastName = "lastName";

            var positionId = 0;

            // Act

            var employee = new Employee(
                firstName, 
                lastName, 
                positionId);

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

            var contract = new Contract(
                subject, 
                address, 
                price, 
                clientId);

            var firstName = "firstName";

            var lastName = "lastName";

            var positionId = 0;

            var employee = new Employee(
                firstName, 
                lastName, 
                positionId);

            // Act

            employee.AddContract(contract);

            // Assert

            Assert.NotEmpty(employee.Contracts);
        }

        [Fact]
        public void When_ChangeFirstName_Expect_ChangeNameWasAddedToEmployee()
        {
            // Arrenge

            var firstName = "firstName";

            var lastName = "lastName";

            var positionId = 0;

            var employee = new Employee(
                firstName, 
                lastName, 
                positionId);

            var newFirstName = "newFirstName";

            // Act

            employee.ChangeFirstName(newFirstName);

            //Assert

            Assert.Equal(newFirstName, employee.FirstName);
        }

        [Fact]
        public void When_ChangeLastName_Expect_ChangeNameWasAddedToEmployee()
        {
            // Arrenge

            var firstName = "firstName";

            var lastName = "lastName";

            var positionId = 0;

            var employee = new Employee(
                firstName,
                lastName,
                positionId);

            var newLastName = "newLastName";

            // Act

            employee.ChangeLastName(newLastName);

            //Assert

            Assert.Equal(newLastName, employee.LastName);
        }

        [Fact]
        public void When_ChangePositionId_Expect_ChangePositionIdWasAddedToEmployee()
        {
            // Arrenge

            var firstName = "firstName";

            var lastName = "lastName";

            var positionId = 0;

            var employee = new Employee(
                firstName, 
                lastName, 
                positionId);

            var newPositionId = 1;

            // Act

            employee.ChangePositionId(newPositionId);

            //Assert

            Assert.Equal(newPositionId, employee.PositionId);
        }
    }
}
