using Login.API.Entities.Users;
using Login.API.Helpers.Interfaces;
using Login.API.Models;
using Login.API.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Login.API.Features.Commands.LoginDict;

public class LoginHandler : IRequestHandler<LoginCommand, AuthDto>
{
    private readonly IUserRepository userRepository;
    private readonly IPasswordHasher<User> passwordHasher;
    private readonly IPasswordHasherExtension passwordHasherExtension;
    private readonly ITokenJwt tokenJwt;

    public LoginHandler(IUserRepository userRepository,
        IPasswordHasher<User> passwordHasher,
        ITokenJwt tokenJwt,
        IPasswordHasherExtension passwordHasherExtension)
    {
        this.userRepository = userRepository;
        this.passwordHasher = passwordHasher;
        this.tokenJwt = tokenJwt;
        this.passwordHasherExtension = passwordHasherExtension;
    }

    public async Task<AuthDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByLogin(request.login, cancellationToken);
        if (user == null)
        {
            throw new KeyNotFoundException(LogUserNotExists(request.login));
        }

        var saltedPassword = passwordHasherExtension.GenerateSaltWithPassowrd(request.password, user.Salt);

        var result = passwordHasher.VerifyHashedPassword(user, user.Password, saltedPassword);
        if (result != PasswordVerificationResult.Success)
        {
            throw new UnauthorizedAccessException(LogPasswordIsIncorrect(request.login));
        }

        var token = tokenJwt.CreateToken(user);

        return new AuthDto(user.Login, token);
    }

    public string LogUserNotExists(string login) => $"User by {login} is not exists";

    public string LogPasswordIsIncorrect(string login) => $"Password for user {login} is incorrect";
}
