using Login.API.Features.Commands.RegisterDict;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Login.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IMediator mediator;

        public LoginController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IResult> Register([FromBody] RegisterCommand request)
        {
            var result = await mediator.Send(request);
            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }

            return Results.Ok();
        }
    }
}
