using System;
using Xunit;
using Crm.Application.Crud.Employee;

namespace Crm.Application.Tests
{
    public class EmployeeCrudTests
    {
        [Fact]
        public async void Task_When_AddNewToDbAsync_Expect_EmployeeWasAddedToDb()
        {
            // Arrenge

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var employeeCrud = new EmployeeCrud(context);

            var employeeParameters = new EmployeeParameters()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                PositionId = 0
            };

            // Act

            await employeeCrud.AddToDbAsync(employeeParameters, cancellationToken);

            // Assert

            Assert.NotNull(context.Employees);
        }

        [Fact]
        public async void Task_When_SelectingFromTheDbAsync_Expect_EmployeeWasSelectFromDb()
        {
            // Arrenge

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var employeeCrud = new EmployeeCrud(context);

            var employeeParameters = new EmployeeParameters()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                PositionId = 0
            };

            int id = await employeeCrud.AddToDbAsync(employeeParameters, cancellationToken);

            // Act

            var employee = await employeeCrud.SelectingFromTheDbAsync(id);

            // Assert

            Assert.Equal(id, employee.Id);
        }

        [Fact]
        public async void Task_When_UpdateInTheDbAsync_Expect_EmployeeWasUpdateInDb()
        {
            // Arrenge

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var employeeCrud = new EmployeeCrud(context);

            var employeeParameters = new EmployeeParameters()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                PositionId = 0
            };

            int id = await employeeCrud.AddToDbAsync(employeeParameters, cancellationToken);

            var newFirstName = "newFirstName";

            var newEmployeeParameters = new EmployeeParameters()
            {
                FirstName = newFirstName,
                LastName = "LastName",
                PositionId = 0
            };

            // Act

            await employeeCrud.UpdateInTheDbAsync(newEmployeeParameters, id, cancellationToken);

            // Assert

            Assert.Equal(newFirstName, context.Employees.Single(c => c.Id == id).FirstName);
        }

        [Fact]
        public async void Task_When_DeleteInTheDbAsync_Expect_EmployeeWasDeleteInDb()
        {
            // Arrenge

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            CancellationToken cancellationToken = cancelTokenSource.Token;

            var context = ConnectionFactory.Generate();

            var employeeCrud = new EmployeeCrud(context);

            var employeeParameters = new EmployeeParameters()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                PositionId = 0
            };

            int id = await employeeCrud.AddToDbAsync(employeeParameters, cancellationToken);

            // Act

            await employeeCrud.DeleteInTheDbAsync(id, cancellationToken);

            // Assert

            Assert.Empty(context.Employees);
        }
    }
}
