using UserService.Localization;
using Volo.Abp.Localization;

namespace UserService.Configurations
{

    public static class LocalizationConfigurationExtension
    {
        public static void ConfigureLocalization(this ServiceConfigurationContext context)
        {
            context.Configure<AbpLocalizationOptions>(options =>
            {
                options.Languages.Clear();
                options.Languages.Add(new LanguageInfo("fa", "fa", "فارسی"));
                options.Languages.Add(new LanguageInfo("en", "en", "English"));
                options.Languages.Add(new LanguageInfo("en-GB", "en-GB", "English (UK)"));

                options.DefaultResourceType = typeof(UserServiceResource);
            });
        }
    }
}