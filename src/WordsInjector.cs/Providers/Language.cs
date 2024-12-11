using MongoDB.Driver;
using System.Text.Json;
using Words.Infrastructure.Persistence;

namespace WordsInjector.cs.Providers;

public static class Language
{
    public const string FilePath = "Dictionary/Language.json";

    public static async Task Load(IMongoContext mongoContext)
    {
        string json = await File.ReadAllTextAsync(FilePath);

        if (!string.IsNullOrEmpty(json))
        {
            var languages = JsonSerializer.Deserialize<List<Words.Domain.Entities.Language>>(json);

            if (languages == null)
            {
                Console.WriteLine("Error: Failed to parse the JSON file.");
                return;
            }

            foreach (var level in languages)
            {
                level.Id = MongoDB.Bson.ObjectId.GenerateNewId();
            }

            await mongoContext.Languages.DeleteManyAsync(Builders<Words.Domain.Entities.Language>.Filter.Empty);
            await mongoContext.Languages.InsertManyAsync(languages);
        }
    }
}
