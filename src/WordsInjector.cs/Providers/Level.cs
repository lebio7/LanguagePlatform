using MongoDB.Driver;
using System.Text.Json;
using Words.Infrastructure.Persistence;

namespace WordsInjector.cs.Providers;

public static class Level
{
    public const string FilePath = "Dictionary/Levels.json";
    public static async Task Load(IMongoContext mongoContext)
    {
        string json = await File.ReadAllTextAsync(FilePath);

        if (!string.IsNullOrEmpty(json))
        {
            var levels = JsonSerializer.Deserialize<List<Words.Domain.Entities.Level>>(json);

            if (levels == null)
            {
                Console.WriteLine("Error: Failed to parse the JSON file.");
                return;
            }

            foreach (var level in levels)
            {
                level.Id = MongoDB.Bson.ObjectId.GenerateNewId();
            }


            await mongoContext.Levels.DeleteManyAsync(Builders<Words.Domain.Entities.Level>.Filter.Empty);
            await mongoContext.Levels.InsertManyAsync(levels);
        }
    }
}
