using Localization.Resources.AbpUi;
using UserService.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace UserService;

[DependsOn(
    typeof(UserServiceApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class UserServiceHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(UserServiceHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<UserServiceResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
