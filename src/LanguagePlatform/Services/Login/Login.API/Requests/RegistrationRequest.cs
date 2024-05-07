using Login.API.Helpers.Enums;

namespace Login.API.Requests;

public record RegistrationRequest(string Login, string Email, string Password, Role role);
