using MediatR;
using Words.Domain.Enums;
using Words.Infrastructure.Repositories;

namespace Words.API.Features.Queries.GetWordsDict
{
    public record GetWordsQuery: IRequest<IReadOnlyList<object>>
    {

    }
    public class GetWordsHandler(IWordRepository wordRepository) : IRequestHandler<GetWordsQuery, IReadOnlyList<object>>
    {
        public async Task<IReadOnlyList<object>> Handle(GetWordsQuery request, CancellationToken cancellationToken)
        {
            var words = await wordRepository.GetWordsWithExtraDetails(new GetWordsWithExtraDetailsFilter((int) LanguageValue.English, null, null));

            return words;
        }
    }
}
