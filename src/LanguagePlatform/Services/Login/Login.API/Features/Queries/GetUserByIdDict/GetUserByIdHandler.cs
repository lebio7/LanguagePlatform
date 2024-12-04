using Login.API.Behaviours;
using Login.API.Helpers.MapperDto;
using Login.API.Models;
using Login.API.Persistence;
using MediatR;

namespace Login.API.Features.Queries.GetUserByIdDict
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUserRepository userRepository;

        public GetUserByIdHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.Id);
            if (user is null) throw new NotFoundException(nameof(user), request.Id);

            return user.MapUser();
        }
    }
}
