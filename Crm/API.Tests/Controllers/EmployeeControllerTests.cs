using System;
using Xunit;
using API.Controllers;
using API.Tests.Connection;
using API.DTOs.Employee;

namespace API.Tests.Controllers
{
    public class EmployeeControllerTests
    {
        [Fact]
        public async void Task_When_CreateNewEmployee_Expect_CreateNewEmployeeWasAddedToDb()
        {
            // Arrange

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var employeeController = new EmployeeController(context);

            var createEmployeeDto = new CreateEmployeeDto()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                PositionId = 0
            };

            // Act

            await employeeController.CreateNewEmployee(createEmployeeDto, cancellationToken);

            // Assert

            Assert.NotNull(context.Employees);
        }

        [Fact]
        public async void Task_When_SelectingEmployee_Expect_SelectingEmployeeWasAddedFromDb()
        {
            // Arrange

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var employeeController = new EmployeeController(context);

            var createEmployeeDto = new CreateEmployeeDto()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                PositionId = 0
            };

            var id = await employeeController.CreateNewEmployee(createEmployeeDto, cancellationToken);

            // Act

            var employee = await employeeController.SelectingEmployee(id.Value);

            // Assert

            Assert.Equal(id, employee.Value.Id);
        }

        [Fact]
        public async void Task_When_UpdateEmployee_Expect_UpdateEmployeeWasAddedInDb()
        {
            // Arrange

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var employeeController = new EmployeeController(context);

            var createEmployeeDto = new CreateEmployeeDto()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                PositionId = 0
            };

            var updateEmployeeDto = new UpdateEmployeeDto()
            {
                NewFirstName = "NewFirstName",
                NewLastName = "NewLastName",
                NewPositionId = 1
            };

            var id = await employeeController.CreateNewEmployee(createEmployeeDto, cancellationToken);

            // Act

            await employeeController.UpdateEmployee(updateEmployeeDto, id.Value, cancellationToken);

            // Assert

            Assert.Equal(updateEmployeeDto.NewFirstName, context.Employees.Single(c => c.Id == id.Value).FirstName);
            Assert.Equal(updateEmployeeDto.NewLastName, context.Employees.Single(c => c.Id == id.Value).LastName);
            Assert.Equal(updateEmployeeDto.NewPositionId, context.Employees.Single(c => c.Id == id.Value).PositionId);
        }

        [Fact]
        public async void Task_When_DeleteEmployee_Expect_DeleteEmployeeWasAddedFromDb()
        {
            // Arrange

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var employeeController = new EmployeeController(context);

            var createEmployeeDto = new CreateEmployeeDto()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                PositionId = 0
            };

            var id = await employeeController.CreateNewEmployee(createEmployeeDto, cancellationToken);

            // Act

            await employeeController.DeleteEmployee(id.Value, cancellationToken);

            // Assert

            Assert.Empty(context.Employees);
        }
    }
}
