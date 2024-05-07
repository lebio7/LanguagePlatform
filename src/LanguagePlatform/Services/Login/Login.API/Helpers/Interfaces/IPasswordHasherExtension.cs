namespace Login.API.Helpers.Interfaces
{
    public interface IPasswordHasherExtension
    {
        string GenerateSalt();
    }
}