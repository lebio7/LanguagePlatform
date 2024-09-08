using Login.API.Behaviours;
using Login.API.Features.Commands.LoginDict;
using Login.API.Features.Commands.RegisterDict;
using Login.API.Features.Queries.GetAllUsersDict;
using Login.API.Features.Queries.GetUserByIdDict;
using Login.API.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Login.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IMediator mediator;

        public LoginController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IResult> Register([FromBody] RegisterCommand request)
        {
            var result = await mediator.Send(request);
            if (result.IsFailure)
            {
                throw new ResultException(result.GetMessage());
            }

            return Results.Ok();
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthDto>> Login([FromBody] LoginCommand request)
        {
            var result = await mediator.Send(request);

            return Ok(result);
        }

        [HttpGet("Users")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<PaginationResult<UserDto>>> GetAllUsers([FromQuery] GetAllUsersQuery query)
        {
            var result = await mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("[action]/{id}")]
        [Authorize(Roles = "ADMIN,USER")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            var res = await mediator.Send(new GetUserByIdQuery(id));

            return Ok(res);
        }
    }
}
