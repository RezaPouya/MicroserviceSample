using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserService.DTOs.inputs;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace UserService.Users
{
    public interface IUserAppService : IApplicationService, ITransientDependency
    {
        Task CreateAsync(UserInputDto inputDto, CancellationToken cancellationToken);

        Task RemoveAsync(Guid id, CancellationToken cancellationToken);


        Task<string> GetAuthorized();
        [Authorize("UserService.Users.Read")]
        Task<string> GetAuthorizedWithPermission();
    }
}