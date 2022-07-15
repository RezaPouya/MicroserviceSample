using IdentityService.AuthUsers;
using IdentityService.AuthUsers.Repositories;
using IdentityService.DTOs.outputs;
using IdentityService.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace IdentityService.Repositories
{
    public class AuthUserRepository : EfCoreRepository<IdentityServiceDbContext, AuthUser, Guid>, IAuthUserRepository
    {
        public AuthUserRepository(IDbContextProvider<IdentityServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<AuthUser> FindAsync(Guid id, CancellationToken cancellationToken)
        {
            return await GetAsync(p => p.Id == id && p.IsDeleted == false, includeDetails: true, cancellationToken: cancellationToken);
        }

        public override async Task<IQueryable<AuthUser>> WithDetailsAsync()
        {
            var queryAble = await GetQueryableAsync();

            return queryAble/*.Include(p => p.SaleOrderItems)*/;
        }

        public async Task<bool> IsExistAsync(string cellphone, CancellationToken cancellationToken)
        {
            var query = await GetQueryableAsync();

            return await query.AsNoTracking()
                .Where(p => p.Cellphone.Equals(cellphone))
                .Where(p => p.IsDeleted == false)
                .AnyAsync(cancellationToken);
        }

        public async Task RemoveAsync(AuthUser user, CancellationToken cancellationToken)
        {
            await base.DeleteAsync(user.Id);
        }

        public async Task<AuthUserOutputDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var query = await WithDetailsAsync();

            return await query.AsNoTracking()
                .Where(p => p.Id == id)
                .Where(p => p.IsDeleted == false)
                .Select(p => new AuthUserOutputDto
                {
                    Id = p.Id,
                    Cellphone = p.Cellphone,
                    Email = p.Email
                }).FirstOrDefaultAsync(cancellationToken);
        }


    }
}