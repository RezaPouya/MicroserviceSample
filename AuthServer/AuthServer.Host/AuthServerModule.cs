using AuthServer.Host.Configurations;
using AuthServer.Host.Configurations.Services;
using AuthServer.Shared;
using AuthServer.Shared.Localization.AuthServer;
using IdentityManagment;
using Localization.Resources.AbpUi;
using Volo.Abp.Account.Localization;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Authorization.Localization;
using Volo.Abp.Emailing;
using Volo.Abp.ExceptionHandling.Localization;
using Volo.Abp.Identity.Localization;
using Volo.Abp.IdentityServer.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.VirtualFileSystem;

namespace AuthServer.Host
{
    [DependsOn(typeof(AbpAutofacModule))]
    //[DependsOn(typeof(AbpCachingStackExchangeRedisModule))]
    [DependsOn(typeof(AbpAspNetCoreSerilogModule))]
    [DependsOn(typeof(AbpBackgroundJobsModule))]
    [DependsOn(typeof(AbpEmailingModule))]
    // ---------------------------------------------------------------------------------
    //[DependsOn(typeof(AbpAccountHttpApiModule))]
    [DependsOn(typeof(AbpAccountApplicationModule))]
    [DependsOn(typeof(AbpAccountWebIdentityServerModule))]
    [DependsOn(typeof(AbpAspNetCoreMvcUiBasicThemeModule))]
    // ---------------------------------------------------------------------------------
    [DependsOn(typeof(IdentityManagmentEntityFrameworkCoreModule))]
    [DependsOn(typeof(AuthServerHostSharedModule))]
    public class AuthServerModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("Identity", typeof(AuthServerResource));
            });

            Configure<AbpBackgroundJobOptions>(options =>
            {
                options.IsJobExecutionEnabled = false;
            });

            PreConfigure<IIdentityServerBuilder>(identityServerBuilder =>
            {
                if (hostingEnvironment.IsDevelopment())
                {
                    identityServerBuilder.AddDeveloperSigningCredential();
                }
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();

            Configure<AbpDbContextOptions>(opts => { opts.UseSqlServer(); });

            ConfigureCommonServices(hostingEnvironment, configuration);

            context.ConfigureCors();
            //context.ConfigureRedis();

            Configure<AbpAuditingOptions>(opts =>
            {
                opts.ApplicationName = AuthServerHostConstants.ApplicationName;
                opts.IsEnabledForGetRequests = false;
            });
        }

        private void ConfigureCommonServices(IWebHostEnvironment hostingEnvironment, IConfiguration configuration)
        {
            ConfigureLocalization();

            //Configure<AbpMultiTenancyOptions>(opts => { opts.IsEnabled = false; });

            Configure<AbpDistributedCacheOptions>(opts => { opts.KeyPrefix = $"{AuthServerHostConstants.ApiName}:"; });

            Configure<AppUrlOptions>(options =>
            {
                options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
                //options.RedirectAllowedUrls.AddRange(configuration["App:RedirectAllowedUrls"].Split(','));
            });

            //Configure<AbpBundlingOptions>(options =>
            //{
            //    options.StyleBundles.Configure(
            //        BasicThemeBundles.Styles.Global,
            //        bundle => { bundle.AddFiles("/global-styles.css"); }
            //    );
            //});

            if (hostingEnvironment.IsDevelopment())
            {
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.AddEmbedded<AuthServerModule>();

                    var root = hostingEnvironment.ContentRootPath;
                    var shared = $"..{Path.DirectorySeparatorChar}AuthServer.Shared";
                    var x = root.Replace("\\AuthServer.Host", "");
                    var x2 = x + $"{Path.DirectorySeparatorChar}AuthServer.Shared";
                    var hostSharedPath = Path.Combine(root, shared );

                    options.FileSets.ReplaceEmbeddedByPhysical<AuthServerHostSharedModule>(x2);
                });
            }
        }

        private void ConfigureLocalization()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                var faLanguage = new LanguageInfo(AuthServerHostConstants.DefaultCultureName,
                    AuthServerHostConstants.DefaultCultureName,
                    "فارسی");

                options.Languages.Clear();
                options.Languages.Add(faLanguage);

                options.DefaultResourceType = typeof(AuthServerResource);

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

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            context.ConfigureMiddlewares();
        }
    }
}