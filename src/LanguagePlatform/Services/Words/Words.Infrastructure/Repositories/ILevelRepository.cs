using Words.Domain.Entities;

namespace Words.Infrastructure.Repositories
{
    public interface ILevelRepository
    {
        Task CreateLevel(Level level);
        Task<Level> GetLevel(string id);
        Task<IEnumerable<Level>> GetLevels();
    }
}