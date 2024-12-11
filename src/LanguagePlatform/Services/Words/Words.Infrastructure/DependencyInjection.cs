using Microsoft.Extensions.DependencyInjection;
using Words.Infrastructure.Persistence;
using Words.Infrastructure.Repositories;

namespace Words.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IMongoContext, MongoContext>();
            services.AddScoped<IWordRepository, WordRepository>();
            services.AddScoped<ILevelRepository, LevelRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            return services;
        }
    }
}
