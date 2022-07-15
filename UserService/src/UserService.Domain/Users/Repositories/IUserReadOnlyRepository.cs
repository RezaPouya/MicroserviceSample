using System;
using System.Threading;
using System.Threading.Tasks;
using UserService.DTOs;
using Volo.Abp.DependencyInjection;

namespace UserService.Users.Repositories
{
    public interface IUserReadOnlyRepository : ITransientDependency
    {
        Task<UserOutputDto> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}