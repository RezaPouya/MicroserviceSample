using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IdentityManagment.EntityFrameworkCore;
using IdentityManagment.Roles;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;

namespace IdentityManagment.Repositories
{

    public class CustomRoleRepository : EfCoreRepository<IdentityManagmentDbContext, IdentityRole, Guid>, ICustomRoleRepository
    {
        public CustomRoleRepository(IDbContextProvider<IdentityManagmentDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<IEnumerable<string>> GetUserRolesNames(IdentityUser user, CancellationToken cancellationToken = default)
        {
            var queryAble = await GetQueryableAsync();

            var userRoleIds = user.Roles.Select(u => u.RoleId).ToList();

            return await queryAble.AsNoTracking()
                .Where(r => userRoleIds.Contains(r.Id))
                .Select(r => r.Name).ToListAsync(cancellationToken);
        }

    }
}