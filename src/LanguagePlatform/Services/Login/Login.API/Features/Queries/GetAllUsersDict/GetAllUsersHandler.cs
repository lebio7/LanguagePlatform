using Login.API.Helpers.MapperDto;
using Login.API.Models;
using Login.API.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Login.API.Features.Queries.GetAllUsersDict
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, PaginationResult<UserDto>>
    {
        private readonly IUserRepository userRepository;

        public GetAllUsersHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<PaginationResult<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            bool isPagination = request.Limit.HasValue;
            var query = userRepository.BuildQueryable(x => string.IsNullOrEmpty(request.Search) || request.Search.ToUpper() == x.Login.ToUpper(),
                z => z.OrderBy(x => x.Login),
                null,
                true,
                request.Limit,
                request.Offset);


            var result = await query.ToListAsync(cancellationToken);
            return new PaginationResult<UserDto>()
            {
                Limit = request.Limit.GetValueOrDefault(),
                Offset = request.Offset.GetValueOrDefault(),
                TotalResult = isPagination ? query.Count() : 0,
                Items = result.Select(z => z.MapUser()).ToList()
            };
        }
    }
}
