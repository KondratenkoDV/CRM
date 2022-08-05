using System;
using Xunit;
using API.Controllers;
using API.DTOs.Employee;
using Moq;
using Application.Services.Employee;
using Domain.Interfaces;

namespace API.Tests.Controllers
{
    public class EmployeeControllerTests
    {
        [Fact]
        public async void Task_When_CreateNewEmployee_Expect_EmployeeWasCreated()
        {
            // Arrange

            var mock = new Mock<IEmployeeService>();

            mock.Setup(e => e.AddAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                CancellationToken.None)).Returns(It.IsAny<Task<int>>());

            var employeeController = new EmployeeController(mock.Object);

            var createEmployeeDto = new CreateEmployeeDto()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                PositionId = 0
            };

            // Act

            await employeeController.CreateNewEmployee(createEmployeeDto, CancellationToken.None);

            // Assert

            mock.Verify(e => e.AddAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                CancellationToken.None),
                Times.Once());
        }

        [Fact]
        public async void Task_When_SelectingEmployee_Expect_EmployeeWasSelected()
        {
            // Arrange

            var mock = new Mock<IEmployeeService>();

            mock.Setup(e => e.SelectingAsync(It.IsAny<int>()))
                .Returns(It.IsAny<Task<Domain.Employee>>());

            var employeeController = new EmployeeController(mock.Object);

            // Act

            await employeeController.SelectingEmployee(It.IsAny<int>());

            // Assert

            mock.Verify(e => e.SelectingAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async void Task_When_UpdateEmployee_Expect_EmployeeWasUpdate()
        {
            // Arrange

            var mock = new Mock<IEmployeeService>();

            mock.Setup(e => e.UpdateAsync(
                It.IsAny<Domain.Employee>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                CancellationToken.None));

            var employeeController = new EmployeeController(mock.Object);

            var updateEmployeeDto = new UpdateEmployeeDto()
            {
                NewFirstName = "NewFirstName",
                NewLastName = "NewLastName",
                NewPositionId = 1
            };

            // Act

            await employeeController.UpdateEmployee(updateEmployeeDto, It.IsAny<int>(), CancellationToken.None);

            // Assert

            mock.Verify(e => e.UpdateAsync(
                It.IsAny<Domain.Employee>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                CancellationToken.None),
                Times.Once());
        }

        [Fact]
        public async void Task_When_DeleteEmployee_Expect_EmployeeWasDeleted()
        {
            // Arrange

            var mock = new Mock<IEmployeeService>();

            mock.Setup(e => e.DeleteAsync(It.IsAny<Domain.Employee>(), CancellationToken.None));

            var employeeController = new EmployeeController(mock.Object);

            // Act

            await employeeController.DeleteEmployee(It.IsAny<int>(), CancellationToken.None);

            // Assert

            mock.Verify(e => e.DeleteAsync(
                It.IsAny<Domain.Employee>(),
                CancellationToken.None),
                Times.Once);
        }
    }
}
