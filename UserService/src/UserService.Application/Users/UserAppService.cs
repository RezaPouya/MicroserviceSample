using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserService.DTOs.inputs;
using UserService.Permissions;

namespace UserService.Users
{
    public class UserAppService : UserServiceAppService, IUserAppService
    {
        private readonly UserManager _userManager;

        public UserAppService(UserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task CreateAsync(UserInputDto inputDto, CancellationToken cancellationToken)
        {
            await _userManager.CreateAsync(inputDto, cancellationToken);
        }

        [Authorize(UserServicePermissions.Users.Read)]
        public async Task<string> GetAuthorizedWithPermission()
        {
            return "This is an authorized access with permission";
        }

        [Authorize()]
        public async Task<string> GetAuthorized()
        {
            return "This is an authorized access with permission";
        }

        public async Task RemoveAsync(Guid id, CancellationToken cancellationToken)
        {
            await _userManager.RemoveAsync(id, cancellationToken);
        }
    }
}