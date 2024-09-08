using FluentValidation;

namespace Login.API.Features.Queries.GetUserByIdDict
{
    public class GetUserByIdValidation : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdValidation()
        {
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
        }
    }
}
