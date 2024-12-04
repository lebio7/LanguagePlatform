using MongoDB.Bson;

namespace Words.Domain.Entities
{
    public class Language
    {
        public ObjectId Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? CultureName { get; set; }
    }
}
