using MongoDB.Bson;

namespace Words.Domain.Entities
{
    public class Category
    {
        public ObjectId Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }
    }
}
