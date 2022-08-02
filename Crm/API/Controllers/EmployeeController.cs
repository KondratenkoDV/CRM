using Microsoft.AspNetCore.Mvc;
using Application.Services.Employee;
using Domain.Interfaces;
using API.DTOs.Employee;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateNewEmployee(
            CreateEmployeeDto createEmployeeDto,
            CancellationToken cancellationToken)
        {
            return Ok(await _employeeService.AddAsync(
                createEmployeeDto.FirstName,
                createEmployeeDto.LastName,
                createEmployeeDto.PositionId,
                cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SelectingEmployeeDto>> SelectingEmployee(int id)
        {
            var employee = await _employeeService.SelectingAsync(id);

            return Ok(new SelectingEmployeeDto()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                PositionId = employee.PositionId,
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(
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

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id, CancellationToken cancellationToken)
        {
            var employee = await _employeeService.SelectingAsync(id);

            await _employeeService.DeleteAsync(employee, cancellationToken);

            return NoContent();
        }
    }
}
