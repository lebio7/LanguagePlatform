using MongoDB.Driver;
using Words.Domain.Entities;
using Words.Infrastructure.Helpers;
using Words.Infrastructure.Persistence;

namespace Words.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly IMongoContext mongoContext;
    public CategoryRepository(IMongoContext mongoContext)
    {
        this.mongoContext = mongoContext;
    }

    public async Task CreateCategory(Category category)
    {
        await mongoContext.Categories.InsertOneAsync(category);
    }

    public async Task<Category> GetCategory(string id)
    {
        return await mongoContext.Categories
            .Find(x => x.Id == id.ParseStringToObjectId())
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Category>> GetCategories()
    {
        return await mongoContext.Categories.Find(x => true).ToListAsync();
    }

    public async Task<IEnumerable<Category>> GetOnlyActiveCategories()
    {
        return await mongoContext.Categories.Find(x => x.IsActive).ToListAsync();
    }
}
