using MediatR;
using Microsoft.AspNetCore.Mvc;
using Words.API.Features.Queries.GetWordsDict;

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


        [HttpGet("{languageValue}")]
        public async Task<IResult> Get(int languageValue)
        {
            var result = await mediator.Send(new GetWordsQuery(languageValue, null, null));

            return Results.Ok(result);
        }

        [HttpGet("{languageValue}/{categoryId}/{levelValue}")]
        public async Task<IResult> GetByFilter(int languageValue, string categoryId, int levelValue)
        {
            var result = await mediator.Send(new GetWordsQuery(languageValue, categoryId, levelValue));

            return Results.Ok(result);
        }
    }
}
