using Login.API.Entities.Users;
using Login.API.Helpers;
using Login.API.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Login.API.Repositories;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    protected readonly UserContext dbContext;

    public UserRepository(UserContext dbContext)
        : base(dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Result> UserExists(string login, string email)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Login.ToUpper() == login.ToUpper()
        || x.Email.ToUpper() == email.ToUpper());

        if (user != null)
        {
            return string.Equals(user.Login, login, StringComparison.InvariantCultureIgnoreCase)
                ? UsersErrors.SameLogin
                : UsersErrors.SameEmail;
        }

        return Error.None;
    }
}
