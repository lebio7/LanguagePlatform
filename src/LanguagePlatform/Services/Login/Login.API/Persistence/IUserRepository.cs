using Login.API.Helpers;

namespace Login.API.Persistence
{
    public interface IUserRepository
    {
        Task<Result> UserExists(string login, string email);
    }
}