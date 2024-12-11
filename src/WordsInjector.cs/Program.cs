using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Words.Infrastructure;
using Words.Infrastructure.Persistence;
using WordsInjector.cs.Providers;

class Program
{
    static async Task Main(string[] args)
    {
        // Utworzenie HostBuilder z konfiguracją i DI
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                // Wczytanie konfiguracji z appsettings.json
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                DependencyInjection.AddInfrastructure(services);
            })
            .Build();

        // Uzyskanie MongoContext z DI
        var mongoContext = host.Services.GetRequiredService<IMongoContext>();

        await Level.Load(mongoContext);

        await Category.Load(mongoContext);

        await Language.Load(mongoContext);
    }
}