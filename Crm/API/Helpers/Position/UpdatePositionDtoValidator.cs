using FluentValidation;
using API.DTOs.Position;

namespace API.Helpers.Position
{
    public class UpdatePositionDtoValidator : AbstractValidator<UpdatePositionDto>
    {
        public UpdatePositionDtoValidator()
        {
            RuleFor(c => c.NewName).Length(2, 50);
        }
    }
}
