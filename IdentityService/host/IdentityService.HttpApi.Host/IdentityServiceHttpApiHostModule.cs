using IdentityService.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Logging;
using System.IO;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Caching;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Swashbuckle;
using Volo.Abp.VirtualFileSystem;

namespace IdentityService;

[DependsOn(
    typeof(IdentityServiceApplicationModule),
    typeof(IdentityServiceEntityFrameworkCoreModule),
    typeof(IdentityServiceHttpApiModule)
    )]
[DependsOn(typeof(AbpAutofacModule))]
[DependsOn(typeof(AbpAuditingModule))]
[DependsOn(typeof(AbpSwashbuckleModule))]

// uncomment if you want to connect to rabbit mq event bus
//[DependsOn(typeof(AbpEventBusRabbitMqModule))]

// un comment if you want to connect to reddis
//[DependsOn(typeof(AbpCachingStackExchangeRedisModule))]
[DependsOn(typeof(AbpEntityFrameworkCoreSqlServerModule))]
[DependsOn(typeof(AbpAspNetCoreSerilogModule))]
[DependsOn(typeof(AbpAuditLoggingEntityFrameworkCoreModule))]
public class IdentityServiceHttpApiHostModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        context.ConfigureUseSqlServer();

        CongigureVirtualFiles(hostingEnvironment);

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
            options.KeyPrefix = "IdentityService:";
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        IdentityModelEventSource.ShowPII = true;
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();
        else
            app.UseHsts();

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
    }

    #region private methods

    private void CongigureVirtualFiles(Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment)
    {
        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<IdentityServiceDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}IdentityService.Domain.Shared", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<IdentityServiceDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}IdentityService.Domain", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<IdentityServiceApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}IdentityService.Application.Contracts", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<IdentityServiceApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}IdentityService.Application", Path.DirectorySeparatorChar)));
            });
        }
    }

    private void AddConventionalController()
    {
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers
                .Create(typeof(IdentityServiceApplicationModule).Assembly, opts =>
                {
                    opts.RootPath = IdentityServiceRemoteServiceConsts.ModuleName;
                });
        });
    }

    private static void UseSwaggerMiddleware(IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "");
            options.OAuthAppName("IdentityServer");
            options.OAuthClientId("identity_service");
            options.OAuthClientSecret("PRWXzxUaWUcuh345asdfS9a#GE3jD+9?");
            options.OAuthScopes(new string[] { "identity_service", "gateway", "user_service" });
        });
    }

    #endregion private methods
}