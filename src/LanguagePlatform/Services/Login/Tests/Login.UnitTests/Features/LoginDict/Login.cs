using FluentAssertions;
using Login.API.Features.Commands.LoginDict;
using Login.API.Features.Queries.GetAllUsersDict;
using Login.API.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http.Headers;
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
            HttpResponseMessage response = await LogInToSystem();

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

        private async Task<HttpResponseMessage> LogInToSystem()
        {
            var command = new LoginCommand(Constants.AdminLogin, Constants.AdminPassword);
            var httpContent = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");

            // act
            var response = await Client.PostAsync(Constants.Urls.Login, httpContent);
            return response;
        }

        [Fact]
        public async Task When_Try_To_Get_List_Without_Authorize_Then_Return_UnAuthorizeCode()
        {
            var userQueryList = new GetAllUsersQuery() { Limit = 5, Offset = 0, Search = Constants.AdminLogin };
            // Serializacja do query string
            var queryParams = $"?Limit={userQueryList.Limit}&Offset={userQueryList.Offset}&Search={Uri.EscapeDataString(userQueryList.Search)}";

            var response = await Client.GetAsync(Constants.Urls.GetAllUsers + queryParams);

            // fact
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
        }
        [Fact]
        public async Task When_Is_Authorize_And_Admin_Login_Exists_Then_Return_List()
        {
            var loginRequest = await LogInToSystem();
            var contentLogin = await loginRequest.Content.ReadAsStringAsync();
            var authDto = JsonConvert.DeserializeObject<AuthDto>(contentLogin);

            var userQueryList = new GetAllUsersQuery() { Limit = 5, Offset = 0, Search = Constants.AdminLogin };
            // Serializacja do query string
            var queryParams = $"?Limit={userQueryList.Limit}&Offset={userQueryList.Offset}&Search={Uri.EscapeDataString(userQueryList.Search)}";
            var request = new HttpRequestMessage(HttpMethod.Get, Constants.Urls.GetAllUsers + queryParams);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authDto?.Token);

            var response = await Client.SendAsync(request);

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();
            var resDto = JsonConvert.DeserializeObject<PaginationResult<UserDto>>(content);
            resDto.Should().NotBeNull();
            resDto?.TotalResult.Should().BeGreaterThan(0);
            resDto?.Items?.Any(x => x.Login == Constants.AdminLogin);
        }
    }
}
