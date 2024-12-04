using Login.API.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;

namespace Login.IntegrationTests
{
    public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly MsSqlContainer msSqlContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2017-latest")
            .Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var descriptor = services.FirstOrDefault(x => x.ServiceType == typeof(DbContextOptions<UserContext>));
                if (descriptor is not null)
                {
                    services.Remove(descriptor);
                }
                services.AddDbContext<UserContext>(options =>
                              options.UseSqlServer(msSqlContainer.GetConnectionString()));
            });
            base.ConfigureWebHost(builder);
        }

        public Task InitializeAsync()
        {
            return msSqlContainer.StartAsync();
        }

        public Task DisposeAsync()
        {
            return msSqlContainer.DisposeAsync().AsTask();
        }
    }
}
