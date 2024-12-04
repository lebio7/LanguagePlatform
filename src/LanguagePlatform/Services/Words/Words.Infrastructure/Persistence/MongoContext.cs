using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Words.Domain.Entities;

namespace Words.Infrastructure.Persistence
{
    public class MongoContext : IMongoContext
    {
        public IMongoCollection<Word> Words { get; }

        public MongoContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["DatabaseSettings:ConnectionString"]);
            var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);
            Words = database.GetCollection<Word>(configuration["DatabaseSettings:CollectionName"]);
        }
    }
}
