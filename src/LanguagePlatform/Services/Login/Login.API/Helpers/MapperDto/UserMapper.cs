using Login.API.Entities.Users;
using Login.API.Models;

namespace Login.API.Helpers.MapperDto
{
    public static class UserMapper
    {
        public static UserDto MapUser(this User user)
        {
            return new UserDto(user.Id, user.Login, user.Email, user.Role);
        }
    }
}
