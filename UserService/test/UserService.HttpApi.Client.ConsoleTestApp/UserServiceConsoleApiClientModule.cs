using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace UserService;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(UserServiceHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class UserServiceConsoleApiClientModule : AbpModule
{

}
