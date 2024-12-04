
using Words.Domain.Entities;

namespace Words.Infrastructure.Repositories
{
    public interface IWordRepository
    {
        Task CreateWord(Word word);
        Task<bool> DeleteWord(string id);
        Task<Word> GetWord(string id);

        Task<IEnumerable<Word>> GetWords();
    }
}