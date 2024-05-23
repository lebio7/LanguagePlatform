using Login.API.Entities.Users;

namespace Login.API.Helpers.Interfaces
{
    public interface ITokenJwt
    {
        string CreateToken(User user);
    }
}
