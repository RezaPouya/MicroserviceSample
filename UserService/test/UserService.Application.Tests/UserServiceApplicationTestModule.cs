using Volo.Abp.Modularity;

namespace UserService;

[DependsOn(
    typeof(UserServiceApplicationModule),
    typeof(UserServiceDomainTestModule)
    )]
public class UserServiceApplicationTestModule : AbpModule
{

}
