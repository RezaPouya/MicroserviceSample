using IdentityManagment.Constants;
using IdentityManagment.Permissions;
using IdentityManagment.Permissions.Permissions;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Uow;

namespace IdentityManagment.IdentityServer.DataSeedServices
{
    [UnitOfWork]
    public class AdminPermissionDataSeedService : ITransientDependency
    {
        private readonly IPermissionDataSeeder _permissionDataSeeder;
        private readonly IPermissionDefinitionManager _permissionDefinitionManager;

        public AdminPermissionDataSeedService(IPermissionDataSeeder permissionDataSeeder,
            IPermissionDefinitionManager permissionDefinitionManager)
        {
            _permissionDataSeeder = permissionDataSeeder;
            _permissionDefinitionManager = permissionDefinitionManager;
        }

        [UnitOfWork]
        public virtual async Task SeedAsync( )
        {
            await SetAdminPermissionAsync();
        }

        public virtual async Task SetAdminPermissionAsync()
        {
            var permissionNames = _permissionDefinitionManager.GetPermissions()
                .Where(p => !p.Providers.Any() || p.Providers.Contains(RolePermissionValueProvider.ProviderName))
                .Select(p => p.Name).ToArray();

            List<string> admin_Default_Permission = GetDefaultPermissionForAdmin();

            List<string> newPermssions = new List<string>();

            foreach (var adp in admin_Default_Permission)
            {
                var isExist = permissionNames.Any(p => p == adp);

                if (permissionNames.Any(p => p == adp))
                {
                    continue;
                }

                newPermssions.Add(adp);
            }

            if (newPermssions.Count > 0)
            {
                await _permissionDataSeeder.SeedAsync(
                    RolePermissionValueProvider.ProviderName,
                    RoleConstants.Admin,
                    newPermssions,
                    null);
            }
        }

        private static List<string> GetDefaultPermissionForAdmin()
        {
            List<string> defaultPermission = AdminPermissions.Abp.GetAll().ToList();
            List<string> userPermissions = UserServicePermissions.GetAll().ToList();


            defaultPermission.AddRange(new List<string>() {
                "SettingManagement.Emailing",
                //"FeatureManagement.ManageHostFeatures",
                //"AbpTenantManagement.Tenants.Create",
                //"AbpTenantManagement.Tenants.ManageConnectionStrings",
                //"AbpTenantManagement.Tenants.Delete",
                //"AbpTenantManagement.Tenants.ManageFeatures",
                //"AbpTenantManagement.Tenants",
                //"AbpTenantManagement.Tenants.Update"
            });

            defaultPermission.AddRange(userPermissions);

            return defaultPermission.Distinct().ToList();
        }
    }
}