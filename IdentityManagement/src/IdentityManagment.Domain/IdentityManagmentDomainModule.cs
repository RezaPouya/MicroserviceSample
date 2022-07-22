using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.AuditLogging;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.PermissionManagement.IdentityServer;

namespace IdentityManagment;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(IdentityManagmentDomainSharedModule)
)]
//--------------------------------------------------------------------------------
[DependsOn(typeof(AbpIdentityDomainModule))]
[DependsOn(typeof(AbpIdentityServerDomainModule))]
[DependsOn(typeof(AbpPermissionManagementDomainIdentityModule))]
[DependsOn(typeof(AbpPermissionManagementDomainIdentityServerModule))]
[DependsOn(typeof(AbpPermissionManagementDomainModule))]
public class IdentityManagmentDomainModule : AbpModule
{

}
