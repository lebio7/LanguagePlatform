using Login.API.Entities.Users;
using Login.API.Helpers;

namespace Login.API.Persistence
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<Result> UserExists(string login, string email);

        Task<User?> GetByLogin(string login, CancellationToken cancellationToken);
    }
}