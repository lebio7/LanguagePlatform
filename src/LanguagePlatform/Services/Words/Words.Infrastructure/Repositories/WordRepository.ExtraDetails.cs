using MongoDB.Driver;
using Words.Domain.Entities;
using Words.Infrastructure.Helpers;

namespace Words.Infrastructure.Repositories
{
    public partial class WordRepository
    {
        public async Task<IReadOnlyList<WordWithDetails>> GetWordsWithExtraDetails(GetWordsWithExtraDetailsFilter filter)
        {
            var result = await mongoContext.Words.Aggregate()
                .Lookup<Word, Category, WordWithDetails>(mongoContext.Categories,
                w => w.CategoryId,
                c => c.Id,
                wc => wc.Category)
                .Lookup<WordWithDetails, Level, WordWithDetails>(mongoContext.Levels,
                w => w.Word.LevelId, l => l.Id,
                wc => new WordWithDetails
                {
                    Word = wc.Word,
                    Level = wc.Level,
                    Category = wc.Category,
                }).ToListAsync();

            return result;
        }
    }

    public record GetWordsWithExtraDetailsFilter(int? categoryId, int? levelId)
    {

    }
}
