using MongoDB.Driver;
using Words.Domain.Entities;
using Words.Infrastructure.Helpers;
using Words.Infrastructure.Persistence;

namespace Words.Infrastructure.Repositories;

public class LevelRepository : ILevelRepository
{
    private readonly IMongoContext mongoContext;
    public LevelRepository(IMongoContext mongoContext)
    {
        this.mongoContext = mongoContext;
    }
    public async Task CreateLevel(Level level)
    {
        await mongoContext.Levels.InsertOneAsync(level);
    }

    public async Task<Level> GetLevel(string id)
    {
        return await mongoContext.Levels
            .Find(x => x.Id == id.ParseStringToObjectId())
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Level>> GetLevels()
    {
        return await mongoContext.Levels.Find(x => true).ToListAsync();
    }

}