using MediatR;
using Words.API.Models;
using Words.Infrastructure.Helpers;
using Words.Infrastructure.Repositories;

namespace Words.API.Features.Queries.GetWordsDict
{
    public record GetWordsQuery: IRequest<IReadOnlyList<TranslatedWordDto>?>
    {
        public  int LanguageValue { get;}

        public string? CategoryId { get;}

        public int? LevelValue { get;}

        public GetWordsQuery(int languageValue,
            string? categoryId,
            int? levelValue)
        {
            LanguageValue = languageValue;
            CategoryId = categoryId;
            LevelValue = levelValue;
        }
    }
    public class GetWordsHandler(IWordRepository wordRepository) : IRequestHandler<GetWordsQuery, IReadOnlyList<TranslatedWordDto>?>
    {
        public async Task<IReadOnlyList<TranslatedWordDto>?> Handle(GetWordsQuery request, CancellationToken cancellationToken)
        {
            var words = await wordRepository.GetWordsWithExtraDetails(new GetWordsWithExtraDetailsFilter(request.LanguageValue, 
                request.CategoryId?.ParseStringToObjectId(),
                request.LevelValue));

            return words?.Select(x=> Map(x)).ToList();
        }

        private static TranslatedWordDto Map(WordWithDetails wordWithDetails) =>
            new TranslatedWordDto()
            {
                Id = wordWithDetails.Word.Id.ToString(),
                Description = wordWithDetails.Word.Description,
                Remark = wordWithDetails.Word.Remark,
                CategoryId = wordWithDetails.Category.Id.ToString(),
                LevelValue = wordWithDetails.Level.Value,
                TranslatedWordId = wordWithDetails.TranslatedWord.Id.ToString(),
                TranslatedWordDescription = wordWithDetails.TranslatedWord.Description,
                TranslatedWordName = wordWithDetails.TranslatedWord.Name,
            };
    }
}
