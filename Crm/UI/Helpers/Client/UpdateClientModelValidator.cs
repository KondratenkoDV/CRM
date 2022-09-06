using FluentValidation;
using UI.Models.Client;

namespace UI.Helpers.Client
{
    public class UpdateClientModelValidator : AbstractValidator<UpdateClientModel>
    {
        public UpdateClientModelValidator()
        {
            RuleFor(c => c.NewName).Length(2, 50);
            RuleFor(c => c.NewRegionCode).Length(2, 2);
            RuleFor(c => c.NewSubscriberNumber).Length(7, 7);
        }
    }
}
