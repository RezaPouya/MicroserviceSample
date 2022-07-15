namespace UserService;

public static class UserServiceDbProperties
{
    public static string DbTablePrefix { get; set; } = "UserService";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "UserService";
}
