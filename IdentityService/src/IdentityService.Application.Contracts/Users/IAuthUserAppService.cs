using IdentityService.DTOs.inputs;
using IdentityService.DTOs.outputs;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace IdentityService.Users
{
    public interface IAuthUserAppService : IApplicationService, ITransientDependency
    {
        Task CreateAsync(AuthUserInputDto inputDto, CancellationToken cancellationToken);
        Task<AuthUserOutputDto> GetAsync(Guid id, CancellationToken cancellationToken);
    }
}