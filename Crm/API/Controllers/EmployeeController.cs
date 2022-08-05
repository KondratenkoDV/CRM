using Microsoft.AspNetCore.Mvc;
using API.DTOs.Employee;
using FluentValidation;
using FluentValidation.Results;
using Domain.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateNewEmployee(
            CreateEmployeeDto createEmployeeDto,
            CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _employeeService.AddAsync(
                    createEmployeeDto.FirstName,
                    createEmployeeDto.LastName,
                    createEmployeeDto.PositionId,
                    cancellationToken));
            }
            catch
            {
                return StatusCodes.Status500InternalServerError;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SelectingEmployeeDto>> SelectingEmployee(int id)
        {
            try
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
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(
            UpdateEmployeeDto updateEmployeeDto,
            int id,
            CancellationToken cancellationToken)
        {
            try
            {
                var employee = await _employeeService.SelectingAsync(id);

                await _employeeService.UpdateAsync(
                    employee,
                    updateEmployeeDto.NewFirstName,
                    updateEmployeeDto.NewLastName,
                    updateEmployeeDto.NewPositionId,
                    cancellationToken);

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id, CancellationToken cancellationToken)
        {
            try
            {
                var employee = await _employeeService.SelectingAsync(id);

                await _employeeService.DeleteAsync(employee, cancellationToken);

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
