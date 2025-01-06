using MongoDB.Bson;

namespace Words.Domain.Entities;

public class TranslatedWord
{
    public ObjectId Id { get; set; }

    public ObjectId? LanguageId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }
}
