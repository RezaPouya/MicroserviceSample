using IdentityService.Users;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserService.DTOs;
using UserService.Users.Repositories;
using Volo.Abp;

namespace UserService.Users
{
    [RemoteService(IsEnabled = false)]
    public class UserReadOnlyAppService : UserServiceAppService, IUserReadOnlyAppService
    {
        // exteranl appservice 
        private readonly IAuthUserAppService _authUserAppService;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;

        public UserReadOnlyAppService(IUserReadOnlyRepository userReadOnlyRepository,
            IAuthUserAppService authUserAppService)
        {
            _userReadOnlyRepository = userReadOnlyRepository;
            _authUserAppService = authUserAppService;
        }

        //  اینجا باید بیزینس مشخص کند که اگر خطایی در هر کدام از سرویس ها بود،
        //  چه نتیجه ای برگشت دهد یا خطا را اعلان کند
        public async Task<UserOutputDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var task1 =  _userReadOnlyRepository.GetUserByIdAsync(id, cancellationToken);
            var task2 =  _authUserAppService.GetAsync(id, cancellationToken);

            await Task.WhenAll(task1, task2);

            var result =  await task1;

            if (result is null)
                return null;

            var authUserResult = await task2;

            if (authUserResult is null)
                return result;

            result.Cellphone = authUserResult.Cellphone;
            result.Email = authUserResult.Email;

            return result;
        }
    }
}