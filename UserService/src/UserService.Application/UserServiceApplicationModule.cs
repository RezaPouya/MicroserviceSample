using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using IdentityService;
using IdentityManagment;
using Volo.Abp.Authorization.Permissions;
using Microsoft.Extensions.DependencyInjection.Extensions;
using UserService.PermissionCheckers;

namespace UserService;

[DependsOn(
    typeof(UserServiceDomainModule),
    typeof(UserServiceApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
    )]
[DependsOn(typeof(IdentityServiceHttpApiClientModule))]
[DependsOn(typeof(IdentityManagmentHttpApiClientModule))]
public class UserServiceApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<UserServiceApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<UserServiceApplicationModule>(validate: true);
        });

        //context.Services.Replace(ServiceDescriptor.Transient<IPermissionChecker, RemotePermissionChecker>());

        context.Services.Replace(ServiceDescriptor.Transient<IPermissionChecker, RemotePermissionChecker2>());
    }
}
