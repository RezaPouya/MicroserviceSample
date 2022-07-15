using Volo.Abp.Reflection;

namespace UserService.Permissions;

public class UserServicePermissions
{
    public const string GroupName = "UserService";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(UserServicePermissions));
    }
}
