namespace IdentityManagment;

public static class IdentityManagmentDbProperties
{
    public static string DbTablePrefix { get; set; } = null;

    public static string IdentityServerDbSchema = "ids";
    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "IdentityManagment";
}
