using Login.API.Entities;
using Login.API.Helpers.Interfaces;
using Login.API.Models;
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

        public LoginController(IPasswordHasherExtension passwordHasherExtension,
            IPasswordHasher<User> passwordHasher,
            ITokenJwt tokenJwt)
        {
            this.passwordHasherExtension = passwordHasherExtension;
            this.passwordHasher = passwordHasher;
            this.tokenJwt = tokenJwt;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
        {
            var salt = passwordHasherExtension.GenerateSalt();
            var saltedPassword = request.Password + salt;

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

            return Ok(new AuthDto(request.Login, token));
        }
    }
}
