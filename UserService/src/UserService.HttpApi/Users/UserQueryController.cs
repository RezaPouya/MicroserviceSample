using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserService.DTOs;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Users;

namespace UserService.Users
{
    [Area(UserServiceRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = UserServiceRemoteServiceConsts.RemoteServiceName)]
    [Route("api/UserService/user/query")]
    public class UserQueryController : IApplicationService, IUserReadOnlyAppService
    {
        private readonly ICurrentUser _currentUser;
        private readonly IUserReadOnlyAppService _userQueryAppService;

        public UserQueryController(ICurrentUser currentUser, IUserReadOnlyAppService userQueryAppService)
        {
            _currentUser = currentUser;
            _userQueryAppService = userQueryAppService;
        }

        [Authorize]
        [HttpGet]
        public async Task<UserOutputDto> GetAsync(CancellationToken cancellationToken)
        {
            return await _userQueryAppService.GetByIdAsync(_currentUser.GetId(), cancellationToken);
        }

        [HttpGet]
        [Route("id/{id}")]
        public async Task<UserOutputDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _userQueryAppService.GetByIdAsync(id, cancellationToken);
        }
    }
}
