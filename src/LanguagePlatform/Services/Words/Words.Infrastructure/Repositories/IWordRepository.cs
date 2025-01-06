
using Words.Domain.Entities;
using Words.Infrastructure.Helpers;

namespace Words.Infrastructure.Repositories
{
    public interface IWordRepository
    {
        Task CreateWord(Word word);
        Task<bool> DeleteWord(string id);
        Task<Word> GetWord(string id);

        Task<IEnumerable<Word>> GetWords();

        Task<IReadOnlyList<WordWithDetails>> GetWordsWithExtraDetails(GetWordsWithExtraDetailsFilter filter);
    }
}