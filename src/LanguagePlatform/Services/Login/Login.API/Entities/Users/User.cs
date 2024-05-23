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

}
