using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserService.EntityFrameworkCore;
using UserService.Users;
using UserService.Users.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace UserService.Repositories
{
    public class UserRepository : EfCoreRepository<UserServiceDbContext, User, Guid>, IUserRepository
    {
        public UserRepository(IDbContextProvider<UserServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<User> FindAsync(Guid id, CancellationToken cancellationToken)
        {
            return await GetAsync(p => p.Id == id, includeDetails: true, cancellationToken: cancellationToken);
        }

        public override async Task<IQueryable<User>> WithDetailsAsync()
        {
            var queryAble = await GetQueryableAsync();

            return queryAble/*.Include(p => p.SaleOrderItems)*/;
        }

        public async Task<bool> IsExistAsync(Guid id, CancellationToken cancellationToken)
        {
            var query = await GetQueryableAsync();
            
            return await query.AsNoTracking().Where(p=> p.Id == id).AnyAsync(cancellationToken);
        }

        public async Task RemoveAsync(User user, CancellationToken cancellationToken)
        {
            await base.DeleteAsync(user.Id);
        }
    }
}