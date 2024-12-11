using MongoDB.Bson;

namespace Words.Domain.Entities
{
    public class Word
    {
        public ObjectId Id { get; set; }

        public string? Description { get; set; }

        public string? Remark { get; set; }

        public ObjectId? CategoryId { get; set; }

        public ObjectId? LevelId { get; set; } 

        public List<TranslatedWord>? TranslatedWords { get; set; }
    }
}
