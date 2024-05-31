using FluentAssertions;
using Login.API.Features.Commands.LoginDict;
using Login.API.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Text;

namespace Login.IntegrationTests.Features.LoginDict
{
    public class Login : BaseSetUp
    {
        public Login(WebApplicationFactory<Program> factory)
            : base(factory)
        {

        }

        [Fact]
        public async Task When_Login_Not_Exists_Then_Return_NotFoundCode()
        {
            // arrange
            var command = new LoginCommand(Guid.NewGuid().ToString(), "Test1234");
            var httpContent = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");

            // act
            var response = await Client.PostAsync(Constants.Urls.Login, httpContent);

            // fact
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task When_Password_Is_Incorrect_Then_Return_Unauthorized()
        {
            // arrange
            var command = new LoginCommand(Constants.AdminLogin, "Test1234");
            var httpContent = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");

            // act
            var response = await Client.PostAsync(Constants.Urls.Login, httpContent);

            // fact
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task When_Login_And_Password_Is_Correct_Then_Return_SuccessCodeAndToken()
        {
            // arrange
            var command = new LoginCommand(Constants.AdminLogin, Constants.AdminPassword);
            var httpContent = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");

            // act
            var response = await Client.PostAsync(Constants.Urls.Login, httpContent);

            // fact
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();
            var authDto = JsonConvert.DeserializeObject<AuthDto>(content);
            authDto.Should().NotBeNull();
            authDto?.Login.Should().NotBeEmpty();
            authDto?.Login.Should().Be(Constants.AdminLogin);
            authDto?.Token.Should().NotBeEmpty();
        }
    }
}
