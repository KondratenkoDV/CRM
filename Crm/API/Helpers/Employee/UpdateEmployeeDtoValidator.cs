using FluentValidation;
using API.DTOs.Employee;

namespace API.Helpers.Employee
{
    public class UpdateEmployeeDtoValidator : AbstractValidator<UpdateEmployeeDto>
    {
        public UpdateEmployeeDtoValidator()
        {
            RuleFor(c => c.NewFirstName).Length(2, 50);
            RuleFor(c => c.NewLastName).Length(2, 50);
        }
    }
}
