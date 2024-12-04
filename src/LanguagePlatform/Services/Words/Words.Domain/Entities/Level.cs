using MongoDB.Bson;

namespace Words.Domain.Entities
{
    public class Level
    {
        public ObjectId Id { get; set; }

        public string? Name { get; set; }

        public int Value { get; set; }
    }
}
