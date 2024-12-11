using MongoDB.Driver;
using System.Text.Json;
using Words.Infrastructure.Persistence;

namespace WordsInjector.cs.Providers;

public static class Category
{
    public const string FilePath = "Dictionary/Categories.json";

    public static async Task Load(IMongoContext mongoContext)
    {
        string json = await File.ReadAllTextAsync(FilePath);

        if (!string.IsNullOrEmpty(json))
        {
            var categories = JsonSerializer.Deserialize<List<Words.Domain.Entities.Category>>(json);

            if (categories == null)
            {
                Console.WriteLine("Error: Failed to parse the JSON file.");
                return;
            }

            foreach (var level in categories)
            {
                level.Id = MongoDB.Bson.ObjectId.GenerateNewId();
            }

            await mongoContext.Categories.DeleteManyAsync(Builders<Words.Domain.Entities.Category>.Filter.Empty);
            // Wstawienie danych do kolekcji Levels
            await mongoContext.Categories.InsertManyAsync(categories);
        }
    }
}
