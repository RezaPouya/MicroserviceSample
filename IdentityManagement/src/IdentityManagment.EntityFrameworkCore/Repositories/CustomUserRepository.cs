using IdentityManagment.EntityFrameworkCore;
using IdentityManagment.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace IdentityManagment.Repositories
{
    public class CustomUserRepository : EfCoreRepository<IdentityManagmentDbContext, IdentityUser, Guid>,
        ICustomUserRepository
    {
        public CustomUserRepository(IDbContextProvider<IdentityManagmentDbContext> dbContextProvider) : base(
            dbContextProvider)
        {
        }

        public async Task<bool> IsUserExist(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
                return false;

            var queryAble = await GetQueryableAsync();

            return await queryAble.AsNoTracking().AnyAsync(p => p.Id == id, cancellationToken);
        }

        public async Task<IdentityUser> GetUserWithRolesAsync(Guid id)
        {
            var queryAble = await this.QueryableWithOnlyRoles();
            return await queryAble.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public override async Task<IQueryable<IdentityUser>> WithDetailsAsync()
        {
            return (await GetQueryableAsync()).IncludeDetails();
        }

        /// <summary>
        /// As No Tracking
        /// </summary>
        private async Task<IQueryable<IdentityUser>> QueryableWithOnlyRoles()
        {
            var queryable = await GetQueryableAsync();

            return queryable
                .Include(x => x.Roles)
                .AsNoTracking();
        }
    }
}