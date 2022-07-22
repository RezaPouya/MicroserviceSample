using Volo.Abp.Reflection;
using System;
using System.Collections.Generic;

namespace IdentityManagment.Definitions
{
    public static class MicroserviceApiResource
    {
        public const string Identity = "identity_service";
        public const string IdentityManagement = "identity_management";
        public const string User = "user_service";

        public static class Gateway
        {
            public const string Internal = "internal_service_gateway";
            public const string Public = "public_gateway";
        }
        
        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(MicroserviceApiResource));
        }
    }
}