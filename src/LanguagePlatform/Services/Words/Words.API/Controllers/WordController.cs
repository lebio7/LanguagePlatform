using MediatR;
using Microsoft.AspNetCore.Mvc;
using Words.Domain.Entities;
using Words.Infrastructure.Repositories;

namespace Words.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IWordRepository wordRepository;

        public WordController(IMediator mediator, IWordRepository wordRepository)
        {
            this.mediator = mediator;
            this.wordRepository = wordRepository;
        }

        [HttpPost("[action]")]
        public async Task<IResult> test()
        {
            await wordRepository.CreateWord(new Word() { CategoryId = null, LevelId = null, Description = "Jak sie amsz", Remark = "test" });

            var a = await wordRepository.GetWords();
            return Results.Ok();
        }
    }
}
