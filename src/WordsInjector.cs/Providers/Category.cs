using MongoDB.Driver;
using System.Text.Json;
using Words.Infrastructure.Persistence;
using WordsInjector.cs.Helpers;

namespace WordsInjector.cs.Providers;

public static class Category
{
    public const string FilePath = "Dictionary/Categories.json";

    public static async Task Load(IMongoContext mongoContext)
    {
        string json = await File.ReadAllTextAsync(FilePath);

        if (!string.IsNullOrEmpty(json))
        {
            var options = new JsonSerializerOptions
            {
                Converters = { new ObjectIdJsonConverter() }
            };

            var categories = JsonSerializer.Deserialize<List<Words.Domain.Entities.Category>>(json, options);

            if (categories == null)
            {
                Console.WriteLine("Error: Failed to parse the JSON file.");
                return;
            }

            await mongoContext.Categories.DeleteManyAsync(Builders<Words.Domain.Entities.Category>.Filter.Empty);

            categories.ForEach(x=> x.IsActive = true);

            // Wstawienie danych do kolekcji Levels
            await mongoContext.Categories.InsertManyAsync(categories);
        }
    }
}
