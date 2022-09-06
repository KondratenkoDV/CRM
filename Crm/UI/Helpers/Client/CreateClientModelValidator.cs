using FluentValidation;
using UI.Models.Client;

namespace UI.Helpers.Client
{
    public class CreateClientModelValidator : AbstractValidator<CreateClientModel>
    {
        public CreateClientModelValidator()
        {
            RuleFor(c => c.Name).Length(2, 50);
            RuleFor(c => c.RegionCode).Length(2, 2);
            RuleFor(c => c.SubscriberNumber).Length(7, 7);
        }
    }
}
