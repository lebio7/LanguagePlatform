using Login.API.Models;
using MediatR;

namespace Login.API.Features.Queries.GetUserByIdDict
{
    public record GetUserByIdQuery(int Id) : IRequest<UserDto>;
}
