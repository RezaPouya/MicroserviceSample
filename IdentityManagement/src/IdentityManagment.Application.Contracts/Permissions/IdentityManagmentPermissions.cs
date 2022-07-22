using Volo.Abp.Reflection;

namespace IdentityManagment.Permissions
{

    public class IdentityManagmentPermissions
    {
        public const string GroupName = "IdentityManagment";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(IdentityManagmentPermissions));
        }
    }
}