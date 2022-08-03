using FluentValidation;
using API.DTOs.Contract;

namespace API.Helpers.Contract
{
    public class UpdateContractDtoValidator : AbstractValidator<UpdateContractDto>
    {
        public UpdateContractDtoValidator()
        {
            RuleFor(c => c.NewSubject).MinimumLength(2);
            RuleFor(c => c.NewAddress).MinimumLength(2);
        }
    }
}
