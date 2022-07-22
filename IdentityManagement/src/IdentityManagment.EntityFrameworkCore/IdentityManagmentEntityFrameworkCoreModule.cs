using IdentityManagment.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using IdentityManagment.Users;
using IdentityManagment.PermissionGrants;
using IdentityManagment.Repositories;

namespace IdentityManagment
{
    [DependsOn(
        typeof(IdentityManagmentDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    //------------------------------------------------------------------------------
    [DependsOn(typeof(AbpPermissionManagementEntityFrameworkCoreModule))]
    [DependsOn(typeof(AbpAuditLoggingEntityFrameworkCoreModule))]
    [DependsOn(typeof(AbpIdentityEntityFrameworkCoreModule))]
    [DependsOn(typeof(AbpIdentityServerEntityFrameworkCoreModule))]
    public class IdentityManagmentEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpDbContextOptions>(options =>
            {
                options.UseSqlServer();
            });

            Configure<AbpDbConnectionOptions>(options =>
            {
                options.Databases.Configure(IdentityManagmentDbProperties.ConnectionStringName, database =>
                {
                    database.MappedConnections.Add("AbpPermissionManagement");
                    database.MappedConnections.Add("AbpIdentity");
                    database.MappedConnections.Add("AbpIdentityServer");
                });
            });

            context.Services.AddAbpDbContext<IdentityManagmentDbContext>(options =>
            {
                options.ReplaceDbContext<IIdentityDbContext>();
                options.ReplaceDbContext<IPermissionManagementDbContext>();
                options.ReplaceDbContext<IIdentityServerDbContext>();

                options.Services.AddTransient<ICustomUserRepository, CustomUserRepository>();
                options.Services.AddTransient<ICustomUserRepository, CustomUserRepository>();
                options.Services.AddTransient<ICustomPermissionGrantRepository, CustomPermissionGrantRepository>();
                options.AddDefaultRepositories(true);
            });
        }
    }
}