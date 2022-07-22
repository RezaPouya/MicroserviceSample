using IdentityManagment.Configurations;
using IdentityManagment.Localization;
using Localization.Resources.AbpUi;
using Volo.Abp.Account.Localization;
using Volo.Abp.Authorization.Localization;
using Volo.Abp.Caching;
using Volo.Abp.Data;
using Volo.Abp.ExceptionHandling.Localization;
using Volo.Abp.Identity.Localization;
using Volo.Abp.IdentityServer.Localization;

namespace IdentityManagment;

[DependsOn(
    typeof(IdentityManagmentApplicationModule),
    typeof(IdentityManagmentEntityFrameworkCoreModule),
    typeof(IdentityManagmentHttpApiModule)
    )]
[DependsOn(typeof(AbpAutofacModule))]
[DependsOn(typeof(AbpAuditingModule))]
[DependsOn(typeof(AbpSwashbuckleModule))]
//[DependsOn(typeof(AbpEventBusRabbitMqModule))]
//[DependsOn(typeof(AbpCachingStackExchangeRedisModule))]
[DependsOn(typeof(AbpEntityFrameworkCoreSqlServerModule))]
[DependsOn(typeof(AbpPermissionManagementEntityFrameworkCoreModule))]
[DependsOn(typeof(AbpAspNetCoreSerilogModule))]
public class IdentityManagmentHttpApiHostModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        context.ConfigureUseSqlServer();

        ConfigureVirtualFileSystemOptions(hostingEnvironment);

        context.ConfigureSwagger();

        context.ConfigureLocalization();

        context.ConfigureAuthentication();

        //  در صورت نیاز به اتصال به ردیس، از کامنت در بیارید
        //context.ConfigureRedis();

        context.ConfigureCors();

        AddConventionalController();

        // if we want to active rabbitMq , we should uncomment this and its module
        //context.ConfigureRabbitMq();

        Configure<AbpDistributedCacheOptions>(options =>
        {
            options.KeyPrefix = "IdentityManagment:";
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors();
        app.UseAuthentication();
        app.UseAbpRequestLocalization();
        app.UseAuthorization();
        UseSwaggerMiddleware(app);
        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();

        SeedData(context);
    }

    private async Task SeedData(ApplicationInitializationContext context)
    {
        using (var scope = context.ServiceProvider.CreateScope())
        {
            scope.ServiceProvider
                .GetRequiredService<IDataSeeder>()
                .SeedAsync().GetAwaiter().GetResult();
        }
    }

    private static void UseSwaggerMiddleware(IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "");
            options.OAuthAppName("AuthServer");
            options.OAuthClientId("identity_management_swagger");
            options.OAuthClientSecret("PRWXzxUaWUcuh345asdfS9a#GE3jD+9?");
            options.OAuthScopes(new string[] { "identity_service", "gateway", "user_service", "identity_management" });
        });
    }

    private void ConfigureVirtualFileSystemOptions(IWebHostEnvironment hostingEnvironment)
    {
        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets
                .ReplaceEmbeddedByPhysical<IdentityManagmentDomainSharedModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}IdentityManagment.Domain.Shared", Path.DirectorySeparatorChar)));
                options.FileSets
                .ReplaceEmbeddedByPhysical<IdentityManagmentDomainModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}IdentityManagment.Domain", Path.DirectorySeparatorChar)));
                options.FileSets
                .ReplaceEmbeddedByPhysical<IdentityManagementApplicationContractsModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}IdentityManagment.Application.Contracts", Path.DirectorySeparatorChar)));
                options.FileSets
                .ReplaceEmbeddedByPhysical<IdentityManagmentApplicationModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}IdentityManagment.Application", Path.DirectorySeparatorChar)));
            });
        }
    }

    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.DefaultResourceType = typeof(IdentityManagmentResource);

            options.Resources.Get<AbpIdentityServerResource>()
                .AddVirtualJson("/Localization/AbpIdentityServer");

            options.Resources.Get<IdentityResource>()
                .AddVirtualJson("/Localization/AbpIdentity");

            options.Resources.Get<AccountResource>()
                .AddVirtualJson("/Localization/AbpAccount");

            options.Resources.Get<AbpAuthorizationResource>()
                .AddVirtualJson("/Localization/AbpAuthorization");

            options.Resources.Get<AbpExceptionHandlingResource>()
                .AddVirtualJson("/Localization/AbpExceptionHandling");

            options.Resources.Get<AbpUiResource>()
                .AddVirtualJson("/Localization/AbpUi");
        });
    }

    /// <summary>
    ///  این متد ، برای ماژول مورد نظر ، کنترلرهای قراردادی را بر اساس
    ///  ApplicationContract
    ///  می سازد و اضافه می کند
    /// </summary>
    private void AddConventionalController()
    {
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers
                .Create(typeof(IdentityManagmentApplicationModule).Assembly,
                    opts => { opts.RootPath = IdentityManagementRemoteServiceConsts.ModuleName; });
        });
    }
}