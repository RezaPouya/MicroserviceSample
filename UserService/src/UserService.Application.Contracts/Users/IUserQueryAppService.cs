using System;
using System.Threading;
using System.Threading.Tasks;
using UserService.DTOs;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace UserService.Users
{
    [RemoteService(IsEnabled = false)]
    public interface IUserReadOnlyAppService : IApplicationService, ITransientDependency
    {
        Task<UserOutputDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    }
}