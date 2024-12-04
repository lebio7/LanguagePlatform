using Login.API.Entities.Users;
using Login.API.Helpers;
using Login.API.Helpers.Interfaces;
using Login.API.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Login.API.Features.Commands.RegisterDict;

public class RegisterHandler : IRequestHandler<RegisterCommand, Result>
{
    private readonly IPasswordHasherExtension passwordHasherExtension;
    private readonly IPasswordHasher<User> passwordHasher;
    private readonly IUserRepository userRepository;
    private readonly ILogger<RegisterHandler> logger;


    public RegisterHandler(IPasswordHasherExtension passwordHasherExtension,
        IPasswordHasher<User> passwordHasher,
        IUserRepository userRepository,
        ILogger<RegisterHandler> logger)
    {
        this.passwordHasherExtension = passwordHasherExtension;
        this.passwordHasher = passwordHasher;
        this.userRepository = userRepository;
        this.logger = logger;
    }
    public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var salt = passwordHasherExtension.GenerateSalt();
        var saltedPassword = passwordHasherExtension.GenerateSaltWithPassowrd(request.Password, salt);

        var errorExists = await userRepository.UserExists(request.Login, request.Email);
        if (errorExists.IsFailure) return errorExists;

        var user = new User(request.Login, request.Email, request.Password)
        {
            Password = passwordHasher.HashPassword(null, saltedPassword),
            Role = request.role,
            Salt = salt,
        };

        await userRepository.AddAsync(user);

        logger.LogInformation(LogCreatedUser(user.Id));

        return Result.Success();
    }

    public string LogCreatedUser(int id) => $"User {id} is successfully created.";
}
