using FluentValidation;
using API.DTOs.Contract;

namespace API.Helpers.Contract
{
    public class CreateContractDtoValidator : AbstractValidator<CreateContractDto>
    {
        public CreateContractDtoValidator()
        {
            RuleFor(c => c.Subject).MinimumLength(2);
            RuleFor(c => c.Address).MinimumLength(2);
        }
    }
}
