using IdentityService;
using IdentityService.AuthUsers;
using IdentityService.AuthUsers.Repositories;
using IdentityService.DTOs.inputs;
using IdentityService.DTOs.outputs;
using IdentityService.Users;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserService.DTOs.inputs;
using UserService.Users;

namespace UserService.Users
{
    public class AuthUserAppService : IdentityServiceAppService, IAuthUserAppService
    {
        private readonly IAuthUserRepository _authUserRepository;

        // this external service which call UserAppService with HttpCientProxy 
        private readonly IUserAppService _userAppService;

        private readonly AuthUserManager _authUserManager;

        public AuthUserAppService(
            IAuthUserRepository userReadOnlyRepository,
            AuthUserManager authUserManager, IUserAppService userAppService)
        {
            _authUserRepository = userReadOnlyRepository;
            _authUserManager = authUserManager;
            _userAppService = userAppService;
        }

        public async Task CreateAsync(AuthUserInputDto inputDto, CancellationToken cancellationToken)
        {
            var authUserId = await _authUserManager.CreateAsync(inputDto, cancellationToken);

            var syncResult = await InsertInUserService(authUserId, inputDto, cancellationToken);

            if (syncResult is true)
                return;

            await _authUserManager.RemoveAsync(authUserId, cancellationToken);

            throw new AuthUserException("The Auth User is not created please try again after few minutes");
        }

        public async Task<AuthUserOutputDto> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _authUserRepository.GetByIdAsync(id, cancellationToken);
        }

        // here we should use saga pattern with double confirmation ,
        // but i don't have time for creating infrastrastur for it right now
        private async Task<bool> InsertInUserService(Guid id , AuthUserInputDto inputDto, CancellationToken cancellation)
        {
            // should use foddy for retry if there is network error 
            try
            {
                var input = new UserInputDto() {
                    Id = id,
                    Fname = inputDto.Fname,
                    Lname = inputDto.Lname
                };

                await _userAppService.CreateAsync(input, cancellation);

                return true;
            }
            catch (Exception ex)
            {
                // we can get the original exception from user-service
                return false;
            }
        }
    }
}