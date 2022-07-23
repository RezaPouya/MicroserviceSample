using UserService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace UserService.Permissions;

public class UserServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(UserServicePermissions.GroupName, L("Permission:UserService"));

        var authorManagement = myGroup.AddPermission(UserServicePermissions.Users.DomainName);
        authorManagement.AddChild(UserServicePermissions.Users.Default);
        authorManagement.AddChild(UserServicePermissions.Users.Read);
        authorManagement.AddChild(UserServicePermissions.Users.Create);
        authorManagement.AddChild(UserServicePermissions.Users.Update);
        authorManagement.AddChild(UserServicePermissions.Users.Delete);
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<UserServiceResource>(name);
    }
}
