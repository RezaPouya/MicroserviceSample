using System.Collections.Generic;
using Volo.Abp.Reflection;

namespace IdentityManagment.Permissions.Permissions.Services
{
    public static class UserServicePermission
    {
        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(UserServicePermission));
        }
    }
}
