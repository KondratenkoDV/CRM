using FluentValidation;
using API.DTOs.Client;

namespace API.Helpers.Client
{
    public class UpdateClientDtoValidator : AbstractValidator<UpdateClientDto>
    {
        public UpdateClientDtoValidator()
        {
            RuleFor(c => c.NewName).Length(2, 50);
            RuleFor(c => c.NewRegionCode).Length(2, 2);
            RuleFor(c => c.NewSubscriberNumber).Length(7, 7);
        }
    }
}
