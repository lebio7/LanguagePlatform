using Login.API.Models;
using MediatR;

namespace Login.API.Features.Commands.LoginDict;

public record LoginCommand(string login, string password) : IRequest<AuthDto>;
