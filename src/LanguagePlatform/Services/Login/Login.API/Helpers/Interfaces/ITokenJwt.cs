using Login.API.Entities;

namespace Login.API.Helpers.Interfaces
{
    public interface ITokenJwt
    {
        string CreateToken(User user);
    }
}
