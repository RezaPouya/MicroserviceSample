using System.Collections.Generic;
using Volo.Abp.Reflection;

namespace IdentityManagment.Permissions.Permissions.Services
{
    public sealed class IdentityServicePermission
    {
        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(IdentityServicePermission));
        }
    }
}