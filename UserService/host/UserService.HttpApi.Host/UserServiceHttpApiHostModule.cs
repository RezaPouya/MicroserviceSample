using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Logging;
using System.IO;
using UserService.PermissionCheckers;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Autofac;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Swashbuckle;
using Volo.Abp.VirtualFileSystem;

namespace UserService;

[DependsOn(
    typeof(UserServiceApplicationModule),
    typeof(UserServiceEntityFrameworkCoreModule),
    typeof(UserServiceHttpApiModule)
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
public class UserServiceHttpApiHostModule : AbpModule
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
                options.FileSets.ReplaceEmbeddedByPhysical<UserServiceDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}UserService.Domain.Shared", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<UserServiceDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}UserService.Domain", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<UserServiceApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}UserService.Application.Contracts", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<UserServiceApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}UserService.Application", Path.DirectorySeparatorChar)));
            });
        }
    }

    private void AddConventionalController()
    {
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers
                .Create(typeof(UserServiceApplicationModule).Assembly, opts =>
                {
                    opts.RootPath = UserServiceRemoteServiceConsts.ModuleName;
                });
        });
    }

    private static void UseSwaggerMiddleware(IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "");
            options.OAuthAppName("AuthServer");
            options.OAuthClientId("user_service_swagger");
            options.OAuthClientSecret("1q2w3e*");
            options.OAuthScopes(new string[] { "user_service", "internal_service_gateway" });
        });
    }

    #endregion private methods
}