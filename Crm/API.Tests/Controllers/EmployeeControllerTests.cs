using System;
using Xunit;
using API.Controllers;
using API.DTOs.Employee;
using Moq;
using Domain.Interfaces;

namespace API.Tests.Controllers
{
    public class EmployeeControllerTests
    {
        private int CreateEmployee(Mock<IEmployeeService> mock)
        {
            return mock.Object.AddAsync(
                "FirstName",
                "LastName",
                0,
                CancellationToken.None).Result;
        }

        [Fact]
        public async void Task_When_CreateNewEmployee_Expect_EmployeeWasCreated()
        {
            // Arrange

            var createEmployeeDto = new CreateEmployeeDto()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                PositionId = 0
            };

            var mock = new Mock<IEmployeeService>();

            mock.Setup(e => e.AddAsync(
                createEmployeeDto.FirstName,
                createEmployeeDto.LastName,
                createEmployeeDto.PositionId,
                CancellationToken.None)).Returns(It.IsAny<Task<int>>());

            var employeeController = new EmployeeController(mock.Object);

            // Act

            await employeeController.CreateNewEmployee(createEmployeeDto, CancellationToken.None);

            // Assert

            mock.Verify(e => e.AddAsync(
                createEmployeeDto.FirstName,
                createEmployeeDto.LastName,
                createEmployeeDto.PositionId,
                CancellationToken.None),
                Times.Once());
        }

        [Fact]
        public async void Task_When_SelectingEmployee_Expect_EmployeeWasSelected()
        {
            // Arrange

            var mock = new Mock<IEmployeeService>();

            var id = CreateEmployee(mock);

            mock.Setup(e => e.SelectingAsync(id))
                .Returns(It.IsAny<Task<Domain.Employee>>());

            var employeeController = new EmployeeController(mock.Object);

            // Act

            await employeeController.SelectingEmployee(id);

            // Assert

            mock.Verify(e => e.SelectingAsync(id), Times.Once);
        }

        [Fact]
        public async void Task_When_UpdateEmployee_Expect_EmployeeWasUpdate()
        {
            // Arrange

            var updateEmployeeDto = new UpdateEmployeeDto()
            {
                NewFirstName = "NewFirstName",
                NewLastName = "NewLastName",
                NewPositionId = 1
            };

            var mock = new Mock<IEmployeeService>();

            var id = CreateEmployee(mock);

            var employee = mock.Object.SelectingAsync(id).Result;

            mock.Setup(e => e.UpdateAsync(
                employee,
                updateEmployeeDto.NewFirstName,
                updateEmployeeDto.NewLastName,
                updateEmployeeDto.NewPositionId,
                CancellationToken.None));

            var employeeController = new EmployeeController(mock.Object);
            
            // Act

            await employeeController.UpdateEmployee(updateEmployeeDto, id, CancellationToken.None);

            // Assert

            mock.Verify(e => e.UpdateAsync(
                employee,
                updateEmployeeDto.NewFirstName,
                updateEmployeeDto.NewLastName,
                updateEmployeeDto.NewPositionId,
                CancellationToken.None),
                Times.Once());
        }

        [Fact]
        public async void Task_When_DeleteEmployee_Expect_EmployeeWasDeleted()
        {
            // Arrange

            var mock = new Mock<IEmployeeService>();

            var id = CreateEmployee(mock);

            var employee = mock.Object.SelectingAsync(id).Result;

            mock.Setup(e => e.DeleteAsync(employee, CancellationToken.None));

            var employeeController = new EmployeeController(mock.Object);

            // Act

            await employeeController.DeleteEmployee(id, CancellationToken.None);

            // Assert

            mock.Verify(e => e.DeleteAsync(
                employee,
                CancellationToken.None),
                Times.Once);
        }
    }
}
