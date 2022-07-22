using Volo.Abp.Reflection;

namespace UserService.Permissions;

public class UserServicePermissions
{
    public const string GroupName = "UserService";

    public static class Users
    {
        public const string DomainName = GroupName + $".{nameof(Users)}";
        public const string Default = DomainName + ".Default";
        public const string Create = DomainName + ".Create";
        public const string Read = DomainName + ".Read";
        public const string Update = DomainName + ".Update";
        public const string Delete = DomainName + ".Delete";
    }

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(UserServicePermissions));
    }
}