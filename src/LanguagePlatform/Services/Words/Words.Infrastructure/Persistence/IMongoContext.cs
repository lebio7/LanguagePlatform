using MongoDB.Driver;
using Words.Domain.Entities;

namespace Words.Infrastructure.Persistence
{
    public interface IMongoContext
    {
        IMongoCollection<Word> Words { get; }
    }
}