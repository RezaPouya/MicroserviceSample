using Volo.Abp.Localization;

namespace IdentityManagment.Localization;

[LocalizationResourceName("IdentityManagment")]
public class IdentityManagmentResource
{
    public static LocalizableString L(string name)
    {
        return LocalizableString.Create<IdentityManagmentResource>(name);
    }
}
