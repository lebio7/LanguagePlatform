using MediatR;
using Microsoft.AspNetCore.Mvc;
using Words.API.Features.Queries.GetWordsDict;
using Words.Domain.Entities;
using Words.Infrastructure.Repositories;

namespace Words.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordController : ControllerBase
    {
        private readonly IMediator mediator;

        public WordController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        public async Task<IResult> Get()
        {
            var result = await mediator.Send(new GetWordsQuery());

            return Results.Ok(result);
        }
    }
}
