using MongoDB.Driver;
using System.Text.Json;
using Words.Infrastructure.Helpers;
using Words.Infrastructure.Persistence;
using WordsInjector.cs.Helpers;
using Domain = Words.Domain.Entities;
namespace WordsInjector.cs.Providers
{
    public static class TranslatedWords
    {
        public const string DictionaryPath = "Dictionary/WordsCategories";

        public const string FileExtension = "*.json";

        public static async Task Load(IMongoContext mongoContext)
        {
            if (!Directory.Exists(DictionaryPath))
                throw new DirectoryNotFoundException($"Directory '{DictionaryPath}' does not exist.");

            var subDirectories = Directory.GetDirectories(DictionaryPath);

            var languages = await mongoContext.Languages.Find(Builders<Domain.Language>.Filter.Empty).ToListAsync();
            var levels = await mongoContext.Levels.Find(Builders<Domain.Level>.Filter.Empty).ToListAsync();

            var jsonFiles = subDirectories.SelectMany(x => Directory.GetFiles(x, FileExtension)).ToList();
            foreach (var jsonFile in jsonFiles)
            {
                try
                {
                    var jsonContent = await File.ReadAllTextAsync(jsonFile);

                    var polishWords = JsonSerializer.Deserialize<List<PolishWord>>(jsonContent);

                    var wordsToAdd = polishWords.Select(x=> x.Mapping(languages, levels)).ToList();

                    await mongoContext.Words.InsertManyAsync(wordsToAdd);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{jsonFile}': {ex.Message}");
                }
            }
        }

        private static Domain.Word Mapping(this PolishWord polishWord,
            List<Domain.Language> languages, 
            List<Domain.Level> levels)
        {
            return new Domain.Word()
            {
                CategoryId = polishWord.CategoryId.ParseStringToObjectId(),
                Description = polishWord.Description,
                Remark = polishWord.Remark,
                LevelId = levels.Find(x => x.Value == polishWord.Level)?.Id ?? null,
                TranslatedWords = polishWord?.TranslatedWords.Select((x => new Domain.TranslatedWord()
                {
                    Description = x.Description,
                    Name = x.Name,
                    LanguageId = languages.Find(z => z.Value == x.Language)?.Id ?? null,
                })).ToList(),
            };
        }
    }
}
