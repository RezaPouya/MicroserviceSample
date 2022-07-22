using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;

namespace IdentityManagment.EntityFrameworkCore;

[ConnectionStringName(IdentityManagmentDbProperties.ConnectionStringName)]
public interface IIdentityManagmentDbContext
     : IIdentityDbContext, IPermissionManagementDbContext, IIdentityServerDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
