using System.Collections.Generic;
using Volo.Abp.Reflection;

namespace IdentityManagment.Definitions
{
    /// <summary>
    /// we decided that have 1-to-1 relation between ApiResource and api scopes
    /// </summary>
    public static class MicroserviceApiScopes
    {
        public const string Identity = MicroserviceApiResource.Identity;
        public const string IdentityManagement = MicroserviceApiResource.IdentityManagement;
        public const string User = MicroserviceApiResource.User;

        public static class Gateway
        {
            public const string Internal = MicroserviceApiResource.Gateway.Internal;
            public const string Public = MicroserviceApiResource.Gateway.Public;
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(MicroserviceApiScopes));
        }
    }
}