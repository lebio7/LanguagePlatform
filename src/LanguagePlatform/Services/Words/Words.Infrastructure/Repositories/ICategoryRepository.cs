using Words.Domain.Entities;

namespace Words.Infrastructure.Repositories
{
    public interface ICategoryRepository
    {
        Task CreateCategory(Category category);
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategory(string id);

        Task<IEnumerable<Category>> GetOnlyActiveCategories();
    }
}