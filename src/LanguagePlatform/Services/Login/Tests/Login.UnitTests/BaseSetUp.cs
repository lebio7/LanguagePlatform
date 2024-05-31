using Microsoft.AspNetCore.Mvc.Testing;

namespace Login.IntegrationTests
{
    public class BaseSetUp : IClassFixture<WebApplicationFactory<Program>>
    {
        protected readonly WebApplicationFactory<Program> factory;
        public BaseSetUp(WebApplicationFactory<Program> factory)
        {
            this.factory = factory;
        }

        protected HttpClient Client => factory.CreateClient();
    }
}
