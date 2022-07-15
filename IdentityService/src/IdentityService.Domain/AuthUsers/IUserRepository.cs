using IdentityService.DTOs.outputs;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace IdentityService.AuthUsers.Repositories
{
    public interface IAuthUserRepository : IRepository<AuthUser, Guid>
    {
        Task<bool> IsExistAsync(string cellphone, CancellationToken cancellationToken);

        Task<AuthUserOutputDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task RemoveAsync(AuthUser user, CancellationToken cancellationToken);
    }
}