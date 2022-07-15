using System;
using System.Threading;
using System.Threading.Tasks;
using UserService.DTOs.inputs;

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

        public async Task RemoveAsync(Guid id, CancellationToken cancellationToken)
        {
            await _userManager.RemoveAsync(id, cancellationToken);
        }
    }
}