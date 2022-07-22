
using AuthServer.Shared.Consants;
using AuthServer.Shared.Localization.AuthServer;
using AuthServer.Shared.Localization.Enum;
using Volo.Abp.AuditLogging;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Localization;
using Volo.Abp.IdentityServer;
using Volo.Abp.IdentityServer.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace AuthServer.Shared
{
    [DependsOn(typeof(AbpLocalizationModule))]
    [DependsOn(typeof(AbpAuditLoggingDomainSharedModule))]
    [DependsOn(typeof(AbpIdentityDomainSharedModule))]
    [DependsOn(typeof(AbpIdentityServerDomainSharedModule))]
    [DependsOn(typeof(AbpPermissionManagementDomainSharedModule))]
    public class AuthServerHostSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AuthServerHostSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<AuthServerResource>(AuthServerHostConstants.DefaultCultureName)
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/AuthServer");

                options.Resources
                    .Add<EnumResource>(AuthServerHostConstants.DefaultCultureName)
                    .AddVirtualJson("/Localization/Enum");

                options.Resources.Get<AbpIdentityServerResource>()
                    .AddVirtualJson("/Localization/AbpIdentityServer");

                options.Resources.Get<IdentityResource>()
                    .AddVirtualJson("/Localization/AbpIdentity");

                options.Resources.Get<AbpValidationResource>()
                    .AddVirtualJson("/Localization/AbpValidation");
                //

                options.Resources.Get<AbpIdentityServerResource>()
                    .AddVirtualJson("/Localization/AbpIdentityServer");

                options.DefaultResourceType = typeof(AuthServerResource);
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("AuthServer", typeof(AuthServerResource));
            });
        }
    }
}