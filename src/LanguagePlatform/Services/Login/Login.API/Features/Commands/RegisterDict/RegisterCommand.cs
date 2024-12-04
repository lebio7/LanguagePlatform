using Login.API.Helpers;
using Login.API.Helpers.Enums;
using MediatR;

namespace Login.API.Features.Commands.RegisterDict;

public record RegisterCommand(string Login, string Email, string Password, Role role) : IRequest<Result>;
