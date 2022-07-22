using IdentityManagment.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace IdentityManagment.Permissions
{
    public class IdentityManagmentPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(IdentityManagmentPermissions.GroupName, L("Permission:IdentityManagment"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<IdentityManagmentResource>(name);
        }
    }
}