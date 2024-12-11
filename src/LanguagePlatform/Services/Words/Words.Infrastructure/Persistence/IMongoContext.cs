using MongoDB.Driver;
using Words.Domain.Entities;

namespace Words.Infrastructure.Persistence
{
    public interface IMongoContext
    {
        IMongoCollection<Word> Words { get; }

        IMongoCollection<Category> Categories { get; }

        IMongoCollection<Level> Levels { get; }

        IMongoCollection<Language> Languages { get; }
    }
}