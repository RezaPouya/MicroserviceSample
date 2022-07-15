using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace UserService.Users.Repositories
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        #region readonly-queries
        Task<bool> IsExistAsync(Guid id, CancellationToken cancellationToken);

        #endregion readonly-queries


        Task RemoveAsync(User user, CancellationToken cancellationToken);
    }
}