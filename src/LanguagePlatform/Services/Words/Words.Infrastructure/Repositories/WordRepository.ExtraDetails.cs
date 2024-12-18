using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Words.Domain.Entities;
using Words.Domain.Enums;
using Words.Domain.Exceptions;
using Words.Infrastructure.Exceptions;
using Words.Infrastructure.Helpers;

namespace Words.Infrastructure.Repositories
{
    public partial class WordRepository
    {
        public async Task<IReadOnlyList<WordWithDetails>> GetWordsWithExtraDetails(GetWordsWithExtraDetailsFilter filter)
        {
            if (!Enum.IsDefined(typeof(LanguageValue), filter.languageValue)) throw new LanguageValueNotExistsException(filter.languageValue);

            var languageId = await mongoContext.Languages.Find(x => x.Value == filter.languageValue).FirstOrDefaultAsync();

            if (languageId is null) throw new NotFoundException(nameof(Language), filter.languageValue);

            var result = await (from word in mongoContext.Words.AsQueryable()
                                join category in mongoContext.Categories.AsQueryable() on word.CategoryId equals category.Id
                                join level in mongoContext.Levels.AsQueryable() on word.LevelId equals level.Id
                                where word.TranslatedWords != null && word.TranslatedWords.Any(z => z.LanguageId == languageId.Id)
                                && (!filter.categoryId.HasValue || word.CategoryId == filter.categoryId)
                                && (!filter.levelValue.HasValue || level.Value == filter.levelValue)
                                select new WordWithDetails()
                                {
                                    Word = word,
                                    Category = category,
                                    Level = level,
                                    TranslatedWord = word.TranslatedWords.FirstOrDefault(x => x.LanguageId == languageId.Id)
                                })
                          .ToListAsync();


            return result;
        }
    }

    public sealed record GetWordsWithExtraDetailsFilter(int languageValue, ObjectId? categoryId, int? levelValue)
    {
    }
}
