using MediatR;
using Microsoft.AspNetCore.Mvc;
using Words.API.Features.Queries.GetCategoriesDict;

namespace Words.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator mediator;

        public CategoryController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IResult> Get()
        {
            var result = await mediator.Send(new GetCategoriesQuery(false));

            return Results.Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IResult> GetOnlyActive()
        {
            var result = await mediator.Send(new GetCategoriesQuery(true));

            return Results.Ok(result);
        }

    }
}
