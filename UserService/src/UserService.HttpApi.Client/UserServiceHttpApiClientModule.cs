using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace UserService;

[DependsOn(
    typeof(UserServiceApplicationContractsModule),
    typeof(AbpHttpClientModule))]


public class UserServiceHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(UserServiceApplicationContractsModule).Assembly,
            UserServiceRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<UserServiceHttpApiClientModule>();
        });

    }
}
