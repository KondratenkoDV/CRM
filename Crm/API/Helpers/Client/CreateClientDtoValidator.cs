using FluentValidation;
using API.DTOs.Client;

namespace API.Helpers.Client
{
    public class CreateClientDtoValidator : AbstractValidator<CreateClientDto>
    {
        public CreateClientDtoValidator()
        {
            RuleFor(c => c.Name).Length(2, 50);
            RuleFor(c => c.RegionCode).Length(2, 2);
            RuleFor(c => c.SubscriberNumber).Length(7, 7);
        }
    }
}
