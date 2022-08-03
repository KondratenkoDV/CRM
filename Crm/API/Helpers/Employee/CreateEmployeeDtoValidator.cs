using FluentValidation;
using API.DTOs.Employee;

namespace API.Helpers.Employee
{
    public class CreateEmployeeDtoValidator : AbstractValidator<CreateEmployeeDto>
    {
        public CreateEmployeeDtoValidator()
        {
            RuleFor(c => c.FirstName).Length(2, 50);
            RuleFor(c => c.LastName).Length(2, 50);
        }
    }
}
