using System.Text.RegularExpressions;

namespace Login.API.Helpers
{
    public static class Constants
    {
        public static readonly Regex EmailRegex = new Regex(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled);

        public static readonly Regex PasswordRegex = new Regex(
        @"^(?=.*[A-Z])(?=.*\d).{6,}$",
        RegexOptions.Compiled);
    }
}
