using System;
using Xunit;
using API.Controllers;
using API.DTOs.Employee;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Application.Services.Employee;

namespace API.Tests.Controllers
{
    public class EmployeeControllerTests : TestCommandBase
    {
        private CreateEmployeeDto GetCreateEmployeeDto()
        {
            return new CreateEmployeeDto()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                PositionId = 0
            };
        }

        [Fact]
        public async void Task_When_CreateNewEmployee_Expect_EmployeeWasCreated()
        {
            // Arrange

            var mock = new Mock<EmployeeService>(Context);

            var employeeController = new EmployeeController(mock.Object);

            var createEmployeeDto = GetCreateEmployeeDto();

            // Act

            await employeeController.CreateNewEmployee(createEmployeeDto, CancellationToken.None);

            // Assert

            Assert.NotNull(Context.Employees);
        }

        [Fact]
        public async void Task_When_SelectingEmployee_Expect_EmployeeWasSelected()
        {
            // Arrange

            var mock = new Mock<EmployeeService>(Context);

            var employeeController = new EmployeeController(mock.Object);

            var createEmployeeDto = GetCreateEmployeeDto();

            var value = await employeeController.CreateNewEmployee(createEmployeeDto, CancellationToken.None);

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

            var mock = new Mock<EmployeeService>(Context);

            var employeeController = new EmployeeController(mock.Object);

            var createEmployeeDto = GetCreateEmployeeDto();

            var updateEmployeeDto = new UpdateEmployeeDto()
            {
                NewFirstName = "NewFirstName",
                NewLastName = "NewLastName",
                NewPositionId = 1
            };

            var value = await employeeController.CreateNewEmployee(createEmployeeDto, CancellationToken.None);

            var result = value.Result as OkObjectResult;

            int id = (int)result.Value;

            // Act

            await employeeController.UpdateEmployee(updateEmployeeDto, id, CancellationToken.None);

            // Assert

            Assert.Equal(updateEmployeeDto.NewFirstName, Context.Employees.Single(c => c.Id == id).FirstName);
            Assert.Equal(updateEmployeeDto.NewLastName, Context.Employees.Single(c => c.Id == id).LastName);
            Assert.Equal(updateEmployeeDto.NewPositionId, Context.Employees.Single(c => c.Id == id).PositionId);
        }

        [Fact]
        public async void Task_When_DeleteEmployee_Expect_EmployeeWasDeleted()
        {
            // Arrange

            var mock = new Mock<EmployeeService>(Context);

            var employeeController = new EmployeeController(mock.Object);

            var createEmployeeDto = GetCreateEmployeeDto();

            var value = await employeeController.CreateNewEmployee(createEmployeeDto, CancellationToken.None);

            var result = value.Result as OkObjectResult;

            int id = (int)result.Value;

            // Act

            await employeeController.DeleteEmployee(id, CancellationToken.None);

            // Assert

            Assert.Empty(Context.Employees);
        }
    }
}
