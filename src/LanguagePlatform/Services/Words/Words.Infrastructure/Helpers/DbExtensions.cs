using MongoDB.Bson;
using Words.Domain.Entities;
using Words.Infrastructure.Exceptions;

namespace Words.Infrastructure.Helpers
{
    public static class DbExtensions
    {
        public static ObjectId ParseStringToObjectId(this string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
                throw new InvalidParseStringToObjectIdException(nameof(Word));

            return objectId;
        }
    }
}
