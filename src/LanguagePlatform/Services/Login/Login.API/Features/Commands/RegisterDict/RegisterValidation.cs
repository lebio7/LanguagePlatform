using FluentValidation;

namespace Login.API.Features.Commands.RegisterDict
{
    public class RegisterValidation : AbstractValidator<RegisterCommand>
    {
        public RegisterValidation()
        {
            RuleFor(x => x.Login).NotNull().NotEmpty();
            RuleFor(x => x.Email).NotNull().NotEmpty();
            RuleFor(x => x.Password).NotNull().NotEmpty();
        }
    }
}
