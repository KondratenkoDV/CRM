using System;
using Xunit;
using Application.Services.Employee;

namespace Application.Tests
{
    public class EmployeeServiceTests : TestCommandBase
    {
        private (string, string, int) GetParameters()
        {
            var firstName = "firstName";

            var lastName = "lastName";

            var positionId = 0;

            return (firstName, lastName, positionId);
        }

        [Fact]
        public async void Task_When_AddAsync_Expect_EmployeeWasAddedToDb()
        {
            // Arrange

            var employeeService = new EmployeeService(Context);

            // Act

            await employeeService.AddAsync(
                GetParameters().Item1,
                GetParameters().Item2,
                GetParameters().Item3,
                CancellationToken.None);

            // Assert

            Assert.NotNull(Context.Employees);
        }

        [Fact]
        public async void Task_When_SelectingAsync_Expect_EmployeeWasSelectFromDb()
        {
            // Arrenge

            var employeeService = new EmployeeService(Context);

            int id = await employeeService.AddAsync(
                GetParameters().Item1,
                GetParameters().Item2,
                GetParameters().Item3,
                CancellationToken.None);

            // Act

            var employee = await employeeService.SelectingAsync(id);

            // Assert

            Assert.Equal(id, employee.Id);
        }

        [Fact]
        public async void Task_When_UpdateAsync_Expect_EmployeeWasUpdateInDb()
        {
            // Arrenge

            var employeeService = new EmployeeService(Context);

            int id = await employeeService.AddAsync(
                GetParameters().Item1,
                GetParameters().Item2,
                GetParameters().Item3,
                CancellationToken.None);
                        
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
                CancellationToken.None);

            // Assert

            Assert.Equal(newFirstName, Context.Employees.Single(c => c.Id == id).FirstName);
            Assert.Equal(newLastName, Context.Employees.Single(c => c.Id == id).LastName);
            Assert.Equal(newPositionId, Context.Employees.Single(c => c.Id == id).PositionId);
        }

        [Fact]
        public async void Task_When_DeleteAsync_Expect_EmployeeWasDeleteInDb()
        {
            // Arrenge

            var employeeService = new EmployeeService(Context);

            int id = await employeeService.AddAsync(
                GetParameters().Item1,
                GetParameters().Item2,
                GetParameters().Item3,
                CancellationToken.None);

            var employee = await employeeService.SelectingAsync(id);

            // Act

            await employeeService.DeleteAsync(employee, CancellationToken.None);

            // Assert

            Assert.Empty(Context.Employees);
        }

        [Fact]
        public async void Task_When_AllAsync_Expect_EmployeesWasSelectFromDb()
        {
            // Arrenge

            var employeeService = new EmployeeService(Context);

            await employeeService.AddAsync(
                GetParameters().Item1,
                GetParameters().Item2,
                GetParameters().Item3,
                CancellationToken.None);

            // Act

            var employees = await employeeService.AllAsync();

            // Assert

            Assert.NotEmpty(employees);
        }
    }
}
