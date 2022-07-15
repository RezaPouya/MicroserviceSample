using Volo.Abp.Localization;

namespace UserService.Localization;

[LocalizationResourceName("UserService")]
public class UserServiceResource
{
	public static LocalizableString L(string name)
	{
		return LocalizableString.Create<UserServiceResource>(name);
	}
}
