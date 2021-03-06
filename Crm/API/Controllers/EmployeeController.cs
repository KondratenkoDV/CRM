using Microsoft.AspNetCore.Mvc;
using Application.Services.Employee;
using Domain.Interfaces;
using API.DTOs.Employee;

namespace API.Controllers
{
    [ApiController]
    [Route("API/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(IDbContext dbContext)
        {
            _employeeService = new EmployeeService(dbContext);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateNewEmployee(
            CreateEmployeeDto createEmployeeDto,
            CancellationToken cancellationToken)
        {
            return await _employeeService.AddAsync(
                createEmployeeDto.FirstName,
                createEmployeeDto.LastName,
                createEmployeeDto.PositionId,
                cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SelectingEmployeeDto>> SelectingEmployee(int id)
        {
            var employee = await _employeeService.SelectingAsync(id);

            return new SelectingEmployeeDto()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                PositionId = employee.PositionId,
            };
        }

        [HttpPut("{id}")]
        public async Task UpdateEmployee(
            UpdateEmployeeDto updateEmployeeDto,
            int id,
            CancellationToken cancellationToken)
        {
            var employee = await _employeeService.SelectingAsync(id);

            await _employeeService.UpdateAsync(
                employee,
                updateEmployeeDto.NewFirstName,
                updateEmployeeDto.NewLastName,
                updateEmployeeDto.NewPositionId,
                cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task DeleteEmployee(int id, CancellationToken cancellationToken)
        {
            var employee = await _employeeService.SelectingAsync(id);

            await _employeeService.DeleteAsync(employee, cancellationToken);
        }
    }
}
