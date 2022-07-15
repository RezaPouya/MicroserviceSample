using UserService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace UserService.Permissions;

public class UserServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(UserServicePermissions.GroupName, L("Permission:UserService"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<UserServiceResource>(name);
    }
}
