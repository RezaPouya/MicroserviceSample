using IdentityManagment.EntityFrameworkCore;
using IdentityManagment.PermissionGrants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.PermissionManagement;

namespace IdentityManagment.Repositories
{
    public class CustomPermissionGrantRepository : EfCoreRepository<IdentityManagmentDbContext, PermissionGrant, Guid>, ICustomPermissionGrantRepository
    {
        public CustomPermissionGrantRepository(IDbContextProvider<IdentityManagmentDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<IEnumerable<PermissionGrant>> GetAllAsync(string providername, CancellationToken cancellationToken)
        {
            var queryAble = await GetQueryableAsync();

            return await queryAble.AsNoTracking()
                .Where(p => p.ProviderName.Equals(providername))
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<PermissionGrant>> GetAllAsync(IEnumerable<string> providerNames, CancellationToken cancellationToken)
        {
            var queryAble = await GetQueryableAsync();

            return await queryAble.AsNoTracking()
                .Where(p => providerNames.Contains(p.ProviderName))
                .ToListAsync(cancellationToken);
        }


        public async Task<bool> IsExistAsync(string providername, CancellationToken cancellationToken)
        {
            var queryAble = await GetQueryableAsync();

            return await queryAble.AsNoTracking().AnyAsync(p => p.ProviderName.Equals(providername));
        }

        public async Task<bool> IsExistAsync(IEnumerable<string> providerNames, CancellationToken cancellationToken)
        {
            var queryAble = await GetQueryableAsync();

            return await queryAble.AsNoTracking().AnyAsync(p => providerNames.Contains(p.ProviderName));
        }
    }
}
