using FluentValidation;
using UI.Models.Position;

namespace UI.Helpers.Position
{
    public class CreatePositionModelValidator : AbstractValidator<CreatePositionModel>
    {
        public CreatePositionModelValidator()
        {
            RuleFor(c => c.Name).Length(2, 50);
        }
    }
}
