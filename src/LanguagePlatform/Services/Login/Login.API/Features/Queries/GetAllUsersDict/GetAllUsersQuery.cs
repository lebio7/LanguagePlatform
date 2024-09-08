using Login.API.Models;
using MediatR;

namespace Login.API.Features.Queries.GetAllUsersDict;

public class GetAllUsersQuery : IRequest<PaginationResult<UserDto>>
{
    public int? Limit { get; set; }

    public int? Offset { get; set; }

    public string? Search { get; set; }
}

