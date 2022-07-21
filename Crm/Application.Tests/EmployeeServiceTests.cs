using System;
using Xunit;
using Application.Tests.Connection;
using Application.Services.Employee;

namespace Application.Tests
{
    public class EmployeeServiceTests
    {
        [Fact]
        public async void Task_When_AddAsync_Expect_EmployeeWasAddedToDb()
        {
            // Arrange

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var employeeService = new EmployeeService(context);

            var firstName = "firstName";

            var lastName = "lastName";

            var positionId = 0;

            // Act

            await employeeService.AddAsync(
                firstName,
                lastName,
                positionId,
                cancellationToken);

            // Assert

            Assert.NotNull(context.Employees);
        }

        [Fact]
        public async void Task_When_SelectingAsync_Expect_EmployeeWasSelectFromDb()
        {
            // Arrenge

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var employeeService = new EmployeeService(context);

            var firstName = "firstName";

            var lastName = "lastName";

            var positionId = 0;

            int id = await employeeService.AddAsync(
                firstName,
                lastName,
                positionId,
                cancellationToken);

            // Act

            var employee = await employeeService.SelectingAsync(id);

            // Assert

            Assert.Equal(id, employee.Id);
        }

        [Fact]
        public async void Task_When_UpdateAsync_Expect_EmployeeWasUpdateInDb()
        {
            // Arrenge

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var employeeService = new EmployeeService(context);

            var firstName = "firstName";

            var lastName = "lastName";

            var positionId = 0;

            int id = await employeeService.AddAsync(
                firstName,
                lastName,
                positionId,
                cancellationToken);
                        
            var newFirstName = "newFirstName";

            var newLastName = "newLastName";

            var newPositionId = 1;

            var employee = await employeeService.SelectingAsync(id);

            // Act

            await employeeService.UpdateAsync(
                employee,
                newFirstName,
                newLastName,
                newPositionId,
                cancellationToken);

            // Assert

            Assert.Equal(newFirstName, context.Employees.Single(c => c.Id == id).FirstName);
            Assert.Equal(newLastName, context.Employees.Single(c => c.Id == id).LastName);
            Assert.Equal(newPositionId, context.Employees.Single(c => c.Id == id).PositionId);
        }

        [Fact]
        public async void Task_When_DeleteAsync_Expect_EmployeeWasDeleteInDb()
        {
            // Arrenge

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var employeeService = new EmployeeService(context);

            var firstName = "firstName";

            var lastName = "lastName";

            var positionId = 0;

            int id = await employeeService.AddAsync(
                firstName,
                lastName,
                positionId,
                cancellationToken);

            var employee = await employeeService.SelectingAsync(id);

            // Act

            await employeeService.DeleteAsync(employee, cancellationToken);

            // Assert

            Assert.Empty(context.Employees);
        }
    }
}
