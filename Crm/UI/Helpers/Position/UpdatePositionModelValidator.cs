using FluentValidation;
using UI.Models.Position;

namespace UI.Helpers.Position
{
    public class UpdatePositionModelValidator : AbstractValidator<UpdatePositionModel>
    {
        public UpdatePositionModelValidator()
        {
            RuleFor(c => c.NewName).Length(2, 50);
        }
    }
}
