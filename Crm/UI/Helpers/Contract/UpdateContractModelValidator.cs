using FluentValidation;
using UI.Models.Contract;

namespace UI.Helpers.Contract
{
    public class UpdateContractModelValidator : AbstractValidator<UpdateContractModel>
    {
        public UpdateContractModelValidator()
        {
            RuleFor(c => c.NewSubject).MinimumLength(2);
            RuleFor(c => c.NewAddress).MinimumLength(2);
        }
    }
}
