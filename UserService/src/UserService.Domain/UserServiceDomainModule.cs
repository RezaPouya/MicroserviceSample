using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace UserService;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(UserServiceDomainSharedModule)
)]
public class UserServiceDomainModule : AbpModule
{

}
