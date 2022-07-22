using System.Collections.Generic;
using Volo.Abp.Reflection;

namespace IdentityManagment.Permissions.Permissions
{
    public static class AdminPermissions
    {
        public static class Abp
        {
            public static string[] GetAll()
            {
                return ReflectionHelper.GetPublicConstantsRecursively(typeof(AdminPermissions.Abp));
            }

            public static class UserMgmt
            {
                /// <summary>
                /// گرفتن تمامی کاربران
                /// </summary>
                public const string GetAllUsers = "AbpIdentity.Users";

                /// <summary>
                /// ساخت کاربر جدید
                /// </summary>
                public const string Create = "AbpIdentity.Users.Create";

                /// <summary>
                /// حذف کاربر
                /// </summary>
                public const string Delete = "AbpIdentity.Users.Delete";

                /// <summary>
                /// ویرایش کاربر
                /// </summary>
                public const string Update = "AbpIdentity.Users.Update";
            }

            public static class RoleMgmt
            {
                /// <summary>
                /// گرفتن تمامی نقش ها
                /// </summary>
                public const string GetAllRoles = "AbpIdentity.Roles";

                /// <summary>
                /// ساخت نقش نو
                /// </summary>
                public const string Create = "AbpIdentity.Roles.Create";

                /// <summary>
                /// حذف نقش
                /// </summary>
                public const string Delete = "AbpIdentity.Roles.Delete";

                /// <summary>
                /// ویرایش نقش
                /// </summary>
                public const string Update = "AbpIdentity.Roles.Update";
            }

            public static class PermissionMgmt
            {
                /// <summary>
                /// مدیریت Permissions ( Authorization Policies ) - نقش ها
                /// </summary>
                public const string Role = "AbpIdentity.Roles.ManagePermissions";

                /// <summary>
                /// مدیریت Permissions ( Authorization Policies ) -  کاربران
                /// </summary>
                public const string User = "AbpIdentity.Users.ManagePermissions";
            }
        }
    }
}
