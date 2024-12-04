using Login.API.Helpers;
using Login.API.Helpers.Enums;

namespace Login.API.Entities.Users;

public class User : EntityBase
{
    public string Login { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Salt { get; set; }

    public bool IsActive { get; set; }

    public Role Role { get; set; }

    public User() { }
    public User(string login,
        string? email,
        string? password)
    {
        if (string.IsNullOrEmpty(login)
            || login.Length < 5)
        {
            throw new UserDomainException("Login is empty or length is less than 5 characters.");
        }
        else if (string.IsNullOrEmpty(email)
            || !Constants.EmailRegex.IsMatch(email))
        {
            throw new UserDomainException("Email is not correct.");
        }
        else if (string.IsNullOrEmpty(password)
            || !Constants.PasswordRegex.IsMatch(password))
        {
            throw new UserDomainException("Password must have more than 6 characters, one uppercase letter, and one number.");
        }

        Login = login;
        Email = email;
        IsActive = true;
    }
}
