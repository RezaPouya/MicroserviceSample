using Volo.Abp.Localization;

namespace AuthServer.Shared.Localization.Enum
{
    [LocalizationResourceName("Enum")]
    public class EnumResource
    {
        public static LocalizableString L(string name)
        {
            return LocalizableString.Create<EnumResource>(name);
        }
    }
}