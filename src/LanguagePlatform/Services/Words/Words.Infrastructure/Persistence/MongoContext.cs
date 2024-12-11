using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Words.Domain.Entities;

namespace Words.Infrastructure.Persistence
{
    public class MongoContext : IMongoContext
    {
        public IMongoCollection<Word> Words { get; }

        public IMongoCollection<Category> Categories { get; }

        public IMongoCollection<Level> Levels { get; }

        public IMongoCollection<Language> Languages { get; }

        public MongoContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["DatabaseSettings:ConnectionString"]);
            var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);
            Words = database.GetCollection<Word>(configuration["DatabaseSettings:CollectionNameWord"]);
            Categories = database.GetCollection<Category>(configuration["DatabaseSettings:CollectionNameCategory"]);
            Levels = database.GetCollection<Level>(configuration["DatabaseSettings:CollectionNameLevel"]);
            Languages = database.GetCollection<Language>(configuration["DatabaseSettings:CollectionNameLanguage"]);
        }
    }
}
