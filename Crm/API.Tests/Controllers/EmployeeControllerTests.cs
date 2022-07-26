using System;
using Xunit;
using API.Controllers;
using API.Tests.Connection;
using API.DTOs.Employee;
using Microsoft.AspNetCore.Mvc;

namespace API.Tests.Controllers
{
    public class EmployeeControllerTests
    {
        [Fact]
        public async void Task_When_CreateNewEmployee_Expect_EmployeeWasCreated()
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
        public async void Task_When_SelectingEmployee_Expect_EmployeeWasSelected()
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

            var value = await employeeController.CreateNewEmployee(createEmployeeDto, cancellationToken);

            var result = value.Result as OkObjectResult;

            int id = (int)result.Value;

            // Act

            var employeeResult = await employeeController.SelectingEmployee(id);

            var employeeValue = employeeResult.Result as OkObjectResult;

            var employee = (SelectingEmployeeDto)employeeValue.Value;

            // Assert

            Assert.Equal(id, employee.Id);
        }

        [Fact]
        public async void Task_When_UpdateEmployee_Expect_EmployeeWasUpdate()
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

            var value = await employeeController.CreateNewEmployee(createEmployeeDto, cancellationToken);

            var result = value.Result as OkObjectResult;

            int id = (int)result.Value;

            // Act

            await employeeController.UpdateEmployee(updateEmployeeDto, id, cancellationToken);

            // Assert

            Assert.Equal(updateEmployeeDto.NewFirstName, context.Employees.Single(c => c.Id == id).FirstName);
            Assert.Equal(updateEmployeeDto.NewLastName, context.Employees.Single(c => c.Id == id).LastName);
            Assert.Equal(updateEmployeeDto.NewPositionId, context.Employees.Single(c => c.Id == id).PositionId);
        }

        [Fact]
        public async void Task_When_DeleteEmployee_Expect_EmployeeWasDeleted()
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

            var value = await employeeController.CreateNewEmployee(createEmployeeDto, cancellationToken);

            var result = value.Result as OkObjectResult;

            int id = (int)result.Value;

            // Act

            await employeeController.DeleteEmployee(id, cancellationToken);

            // Assert

            Assert.Empty(context.Employees);
        }
    }
}
