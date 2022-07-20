using Microsoft.AspNetCore.Mvc;
using Application.Services.Employee;
using API.Helpers;
using API.Models;

namespace API.Controllers
{
    public class EmployeeController : Controller
    {
        [HttpPost]
        public async Task<int> CreateNewEmployee(
            EmployeeModel employeeModel,
            CancellationToken cancellationToken)
        {
            var employeeService = new EmployeeService(CompoundDb.Compound());

            return await employeeService.AddAsync(
                employeeModel.FirstName,
                employeeModel.LastName,
                employeeModel.PositionId,
                cancellationToken);
        }

        [HttpPost]
        public async Task<EmployeeModel> SelectingEmployee(int id)
        {
            var employeeService = new EmployeeService(CompoundDb.Compound());

            var employee = await employeeService.SelectingAsync(id);

            return new EmployeeModel()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                PositionId = employee.PositionId,
            };
        }

        [HttpPost]
        public async Task UpdateEmployee(
            EmployeeModel employeeModel,
            int id,
            CancellationToken cancellationToken)
        {
            var employeeService = new EmployeeService(CompoundDb.Compound());

            var employee = await employeeService.SelectingAsync(id);

            await employeeService.UpdateAsync(
                employee,
                employeeModel.FirstName,
                employeeModel.LastName,
                employeeModel.PositionId,
                cancellationToken);
        }

        [HttpPost]
        public async Task DeleteEmployee(int id, CancellationToken cancellationToken)
        {
            var employeeService = new EmployeeService(CompoundDb.Compound());

            var employee = await employeeService.SelectingAsync(id);

            await employeeService.DeleteAsync(employee, cancellationToken);
        }
    }
}
