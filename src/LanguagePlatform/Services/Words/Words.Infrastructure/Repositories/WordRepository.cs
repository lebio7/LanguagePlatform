using MongoDB.Driver;
using Words.Domain.Entities;
using Words.Infrastructure.Helpers;
using Words.Infrastructure.Persistence;

namespace Words.Infrastructure.Repositories
{
    public class WordRepository : IWordRepository
    {
        private readonly IMongoContext mongoContext;
        public WordRepository(IMongoContext mongoContext)
        {
            this.mongoContext = mongoContext;
        }

        public async Task CreateWord(Word word)
        {
            await mongoContext.Words.InsertOneAsync(word);
        }

        public async Task<bool> DeleteWord(string id)
        {
            FilterDefinition<Word> filter = Builders<Word>.Filter.Eq(p => p.Id, id.ParseStringToObjectId());

            DeleteResult deleteResult = await mongoContext
                                                .Words
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public async Task<Word> GetWord(string id)
        {
            return await mongoContext.Words
                .Find(x => x.Id == id.ParseStringToObjectId())
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Word>> GetWords()
        {
            return await mongoContext.Words.Find(x => true).ToListAsync();
        }
    }
}
