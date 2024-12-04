using FluentValidation;

namespace Login.API.Features.Commands.LoginDict
{
    public class LoginValidation : AbstractValidator<LoginCommand>
    {
        public LoginValidation()
        {
            RuleFor(x=> x.login).NotNull().NotEmpty();
            RuleFor(x => x.password).NotNull().NotEmpty();
        }
    }
}
