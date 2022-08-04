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

        private readonly IValidator<CreateEmployeeDto> _createValidator;

        private readonly IValidator<UpdateEmployeeDto> _updateValidator;

        public EmployeeController(
            IEmployeeService employeeService,
            IValidator<CreateEmployeeDto> createValidator,
            IValidator<UpdateEmployeeDto> updateValidator)
        {
            _employeeService = employeeService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateNewEmployee(
            CreateEmployeeDto createEmployeeDto,
            CancellationToken cancellationToken)
        {
            ValidationResult result = await _createValidator.ValidateAsync(createEmployeeDto);

            if (!result.IsValid)
            {
                return NotFound();
            }

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
            ValidationResult result = await _updateValidator.ValidateAsync(updateEmployeeDto);

            if (!result.IsValid)
            {
                return NotFound();
            }

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
