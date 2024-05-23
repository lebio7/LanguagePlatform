namespace Login.API.Entities.Users
{
    public static class UsersErrors
    {
        public static readonly Error SameLogin = new Error(nameof(UsersErrors), "Login is exists");
        public static readonly Error SameEmail = new Error(nameof(UsersErrors), "Email is exists");
    }
}
