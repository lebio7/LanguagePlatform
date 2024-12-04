using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Login.IntegrationTests
{
    public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
    {
        private readonly IServiceScope scope;
        protected readonly ISender Sender;

        protected readonly IntegrationTestWebAppFactory factory;

        protected HttpClient Client => factory.CreateClient();

        protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
        {
            this.factory = factory;
            scope = factory.Services.CreateScope();
            Sender = scope.ServiceProvider.GetRequiredService<ISender>();
        }
    }
}
