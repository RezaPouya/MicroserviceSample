using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.IdentityServer.ApiResources;
using Volo.Abp.IdentityServer.ApiScopes;
using Volo.Abp.IdentityServer.Clients;
using Volo.Abp.IdentityServer.Devices;
using Volo.Abp.IdentityServer.Grants;
using Volo.Abp.IdentityServer.IdentityResources;

namespace IdentityManagment.EntityFrameworkCore;

public static class IdentityManagmentDbContextModelCreatingExtensions
{
    public static void ConfigureIdentityManagment(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.ConfigureIdentity();
        builder.ConfigureIdentityServer();
        builder.ConfigurePermissionManagement();


        builder.Entity<IdentityUserRole>(b =>
        {
            b.ToTable(IdentityManagmentDbProperties.DbTablePrefix + "UserRoles",
                IdentityManagmentDbProperties.DbSchema);
        });

        builder.Entity<IdentityClaimType>(b =>
        {
            b.ToTable(IdentityManagmentDbProperties.DbTablePrefix + "ClaimTypes",
                IdentityManagmentDbProperties.DbSchema);
        });

        builder.Entity<IdentityUserClaim>(b =>
        {
            b.ToTable(IdentityManagmentDbProperties.DbTablePrefix + "UserClaims",
                IdentityManagmentDbProperties.DbSchema);
        });

        builder.Entity<IdentityRoleClaim>(b =>
        {
            b.ToTable(IdentityManagmentDbProperties.DbTablePrefix + "RoleClaims",
                IdentityManagmentDbProperties.DbSchema);
        });

        builder.Entity<IdentityUser>(b =>
        {
            b.ToTable(IdentityManagmentDbProperties.DbTablePrefix + "Users", IdentityManagmentDbProperties.DbSchema);
        });

        builder.Entity<IdentityRole>(b =>
        {
            b.ToTable(IdentityManagmentDbProperties.DbTablePrefix + "Roles", IdentityManagmentDbProperties.DbSchema);
        });

        builder.Entity<OrganizationUnit>(b =>
        {
            b.ToTable(IdentityManagmentDbProperties.DbTablePrefix + "OrganizationUnits",
                IdentityManagmentDbProperties.DbSchema);
        });

        builder.Entity<OrganizationUnitRole>(b =>
        {
            b.ToTable(IdentityManagmentDbProperties.DbTablePrefix + "OrganizationUnitRoles",
                IdentityManagmentDbProperties.DbSchema);
        });

        builder.Entity<IdentityUserLogin>(b =>
        {
            b.ToTable(IdentityManagmentDbProperties.DbTablePrefix + "UserLogins",
                IdentityManagmentDbProperties.DbSchema);
        });

        builder.Entity<IdentityUserOrganizationUnit>(b =>
        {
            b.ToTable(IdentityManagmentDbProperties.DbTablePrefix + "UserOrganizationUnits",
                IdentityManagmentDbProperties.DbSchema);
        });

        builder.Entity<IdentityUserToken>(b =>
        {
            b.ToTable(IdentityManagmentDbProperties.DbTablePrefix + "UserTokens",
                IdentityManagmentDbProperties.DbSchema);
        });

        builder.Entity<IdentitySecurityLog>(b =>
        {
            b.ToTable(IdentityManagmentDbProperties.DbTablePrefix + "SecurityLogs",
                IdentityManagmentDbProperties.DbSchema);
        });

        builder.Entity<IdentityLinkUser>(b =>
        {
            b.ToTable(IdentityManagmentDbProperties.DbTablePrefix + "LinkUsers",
                IdentityManagmentDbProperties.DbSchema);
        });

        builder.Entity<PermissionGrant>(b =>
        {
            b.ToTable(IdentityManagmentDbProperties.DbTablePrefix + "PermissionGrants",
                IdentityManagmentDbProperties.DbSchema);
        });


        #region IdentityServer

        builder.Entity<ApiResource>(b =>
        {
            b.ToTable("ApiResources", IdentityManagmentDbProperties.IdentityServerDbSchema);
        });
        builder.Entity<ApiResourceSecret>(b =>
        {
            b.ToTable("ApiResourceSecrets", IdentityManagmentDbProperties.IdentityServerDbSchema);
        });
        builder.Entity<ApiResourceScope>(b =>
        {
            b.ToTable("ApiResourceScopes", IdentityManagmentDbProperties.IdentityServerDbSchema);
        });
        builder.Entity<ApiResourceProperty>(b =>
        {
            b.ToTable("ApiResourceProperties", IdentityManagmentDbProperties.IdentityServerDbSchema);
        });
        builder.Entity<ApiScope>(b =>
        {
            b.ToTable("ApiScopes", IdentityManagmentDbProperties.IdentityServerDbSchema);
        });

        builder.Entity<ApiScopeClaim>(b =>
        {
            b.ToTable("ApiScopeClaims", IdentityManagmentDbProperties.IdentityServerDbSchema);
        });
        builder.Entity<ApiScopeProperty>(b =>
        {
            b.ToTable("ApiScopeProperties", IdentityManagmentDbProperties.IdentityServerDbSchema);
        });
        builder.Entity<IdentityResource>(b =>
        {
            b.ToTable("IdentityResources", IdentityManagmentDbProperties.IdentityServerDbSchema);
        });
        builder.Entity<IdentityResourceClaim>(b =>
        {
            b.ToTable("IdentityResourceClaims", IdentityManagmentDbProperties.IdentityServerDbSchema);
        });
        builder.Entity<IdentityResourceProperty>(b =>
        {
            b.ToTable("IdentityResourceProperties", IdentityManagmentDbProperties.IdentityServerDbSchema);
        });
        builder.Entity<Client>(b =>
        {
            b.ToTable("Clients", IdentityManagmentDbProperties.IdentityServerDbSchema);
        });

        builder.Entity<ClientGrantType>(b =>
        {
            b.ToTable("ClientGrantTypes", IdentityManagmentDbProperties.IdentityServerDbSchema);
        });
        builder.Entity<ClientRedirectUri>(b =>
        {
            b.ToTable("ClientRedirectUris", IdentityManagmentDbProperties.IdentityServerDbSchema);
        });
        builder.Entity<ClientPostLogoutRedirectUri>(b =>
        {
            b.ToTable("ClientPostLogoutRedirectUris", IdentityManagmentDbProperties.IdentityServerDbSchema);
        });
        builder.Entity<ClientScope>(b =>
        {
            b.ToTable("ClientScopes", IdentityManagmentDbProperties.IdentityServerDbSchema);
        });
        builder.Entity<ClientSecret>(b =>
        {
            b.ToTable("ClientSecrets", IdentityManagmentDbProperties.IdentityServerDbSchema);
        });
        builder.Entity<ClientClaim>(b =>
        {
            b.ToTable("ClientClaims", IdentityManagmentDbProperties.IdentityServerDbSchema);
        });
        builder.Entity<ClientIdPRestriction>(b =>
        {
            b.ToTable("ClientIdPRestrictions", IdentityManagmentDbProperties.IdentityServerDbSchema);
        });
        builder.Entity<ClientCorsOrigin>(b =>
        {
            b.ToTable("ClientCorsOrigins", IdentityManagmentDbProperties.IdentityServerDbSchema);
        });
        builder.Entity<ClientProperty>(b =>
        {
            b.ToTable("ClientProperties", IdentityManagmentDbProperties.IdentityServerDbSchema);
        });
        builder.Entity<PersistedGrant>(b =>
        {
            b.ToTable("PersistedGrants", IdentityManagmentDbProperties.IdentityServerDbSchema);
        });
        builder.Entity<DeviceFlowCodes>(b =>
        {
            b.ToTable("DeviceFlowCodes", IdentityManagmentDbProperties.IdentityServerDbSchema);
        });
        builder.Entity<ApiResourceClaim>(b =>
        {
            b.ToTable("ApiResourceClaims", IdentityManagmentDbProperties.IdentityServerDbSchema);
        });

        #endregion
    }
}
