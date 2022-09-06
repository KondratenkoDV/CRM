using FluentValidation;
using UI.Models.Employee;

namespace UI.Helpers.Employee
{
    public class CreateEmployeeModelValidator : AbstractValidator<CreateEmployeeModel>
    {
        public CreateEmployeeModelValidator()
        {
            RuleFor(c => c.FirstName).Length(2, 50);
            RuleFor(c => c.LastName).Length(2, 50);
        }
    }
}
