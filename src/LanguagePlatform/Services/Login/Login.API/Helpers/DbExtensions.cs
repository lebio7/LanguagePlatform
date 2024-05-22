using Login.API.Entities;
using Login.API.Helpers.Enums;
using Login.API.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Login.API.Helpers;

public static class DbExtensions
{
    public static IHost MigrateDatabase<TContext>(this IHost host,
                                        Action<TContext, IServiceProvider> seeder,
                                        int? retry = 0) where TContext : DbContext
    {
        int retryForAvailability = retry.Value;

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<TContext>>();
            var context = services.GetService<TContext>();

            try
            {
                logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);

                InvokeSeeder(seeder, context, services);

                logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(TContext).Name);
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(TContext).Name);

                if (retryForAvailability < 50)
                {
                    retryForAvailability++;
                    System.Threading.Thread.Sleep(2000);
                    MigrateDatabase<TContext>(host, seeder, retryForAvailability);
                }
            }
        }
        return host;
    }

    private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder,
                                                TContext context,
                                                IServiceProvider services)
                                                where TContext : DbContext
    {
        context.Database.Migrate();
        seeder(context, services);
    }

    public static async Task SeedAsync(UserContext userContext, ILogger<UserContext> logger)
    {
        if (!userContext.Users.Any())
        {
            userContext.Users.Add(GetAdminUser());
            await userContext.SaveChangesAsync();
            logger.LogInformation("Seed database associated with context {DbContextName}", typeof(UserContext).Name);
        }
    }

    private static User GetAdminUser() =>
    new User
    {
        IsActive = true,
        Login = "admin",
        Password = "AQAAAAIAAYagAAAAEAL4r/3n77GTboDHUdJr3lPuT6+/6OsZvC08ka0rd9quOnAgvytIONfBRg+c0hnMpA==",
        Role = Role.ADMIN,
        Email = "admin@admin.pl",
        Salt = "kZk3SONJMkW3MSdWuubjmg==",
    };
}
