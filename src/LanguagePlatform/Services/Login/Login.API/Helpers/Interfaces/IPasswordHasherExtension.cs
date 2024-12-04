namespace Login.API.Helpers.Interfaces
{
    public interface IPasswordHasherExtension
    {
        string GenerateSalt();

        string GenerateSaltWithPassowrd(string password, string salt);
    }
}