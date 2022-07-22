using System.Collections.Generic;

namespace IdentityManagment.Constants
{
    public static class RoleConstants
    {
        public static List<string> GetAll()
        {
            return new List<string>
            {
                Admin , Customer
            };
        }

        public const string Admin = "admin";
        public const string Customer = "customer";
    }
}