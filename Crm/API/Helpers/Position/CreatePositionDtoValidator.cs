using FluentValidation;
using API.DTOs.Position;

namespace API.Helpers.Position
{
    public class CreatePositionDtoValidator : AbstractValidator<CreatePositionDto>
    {
        public CreatePositionDtoValidator()
        {
            RuleFor(c => c.Name).Length(2, 50);
        }
    }
}
