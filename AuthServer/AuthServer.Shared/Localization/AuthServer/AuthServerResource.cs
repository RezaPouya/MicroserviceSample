using Volo.Abp.Localization;

namespace AuthServer.Shared.Localization.AuthServer
{
    [LocalizationResourceName("AuthServer")]
    public class AuthServerResource
    {
        public static LocalizableString L(string name)
        {
            return LocalizableString.Create<AuthServerResource>(name);
        }
    }
}