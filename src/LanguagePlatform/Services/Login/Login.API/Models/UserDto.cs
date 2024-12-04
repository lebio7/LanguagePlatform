using Login.API.Helpers.Enums;

namespace Login.API.Models;

public record UserDto(int Id, string? Login, string? Email, Role role);

