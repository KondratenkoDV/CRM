using FluentValidation;
using UI.Models.Employee;

namespace UI.Helpers.Employee
{
    public class UpdateEmployeeModelValidator : AbstractValidator<UpdateEmployeeModel>
    {
        public UpdateEmployeeModelValidator()
        {
            RuleFor(c => c.NewFirstName).Length(2, 50);
            RuleFor(c => c.NewLastName).Length(2, 50);
        }
    }
}
