using Login.API.Entities.Users;
using Login.API.Helpers.Interfaces;
using Login.API.Models;
using Login.API.Persistence;
using Login.API.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Login.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IPasswordHasherExtension passwordHasherExtension;
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly ITokenJwt tokenJwt;
        private readonly IUserRepository userRepository;

        public LoginController(IPasswordHasherExtension passwordHasherExtension,
            IPasswordHasher<User> passwordHasher,
            ITokenJwt tokenJwt,
            IUserRepository userRepository)
        {
            this.passwordHasherExtension = passwordHasherExtension;
            this.passwordHasher = passwordHasher;
            this.tokenJwt = tokenJwt;
            this.userRepository = userRepository;
        }

        [HttpPost("[action]")]
        public async Task<IResult> Register([FromBody] RegistrationRequest request)
        {
            var salt = passwordHasherExtension.GenerateSalt();
            var saltedPassword = request.Password + salt;

            var errorExists = await userRepository.UserExists(request.Login, request.Email);
            if (errorExists.IsFailure)
            {
                return Results.BadRequest(errorExists.Error);
            }

            var user = new User
            {
                Login = request.Login,
                Email = request.Email,
                Password = passwordHasher.HashPassword(null, saltedPassword),
                Role = request.role,
                Salt = salt,
                IsActive = true,
            };

            var token = tokenJwt.CreateToken(user);

            return Results.Ok(new AuthDto(request.Login, token));
        }
    }
}
