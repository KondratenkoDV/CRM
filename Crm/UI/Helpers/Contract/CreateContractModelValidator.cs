using FluentValidation;
using UI.Models.Contract;

namespace UI.Helpers.Contract
{
    public class CreateContractModelValidator : AbstractValidator<CreateContractModel>
    {
        public CreateContractModelValidator()
        {
            RuleFor(c => c.Subject).MinimumLength(2);
            RuleFor(c => c.Address).MinimumLength(2);
        }
    }
}
