namespace Login.API.Entities.Users;

public class UserDomainException : ArgumentException
{
    public UserDomainException(string message)
        : base(message)
    {
    }
}
