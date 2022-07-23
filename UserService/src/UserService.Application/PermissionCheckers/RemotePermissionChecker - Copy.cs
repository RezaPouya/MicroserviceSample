using Volo.Abp.ObjectMapping;
using IdentityManagment.PermissionCheckers;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Security.Principal;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Users;
using System.Threading.Tasks;
using IdentityManagment.PermissionGrants;
using IdentityManagment.PermissionGrants.Dto;
using System.Collections.Generic;
using System;

namespace UserService.PermissionCheckers
{
    public class RemotePermissionChecker2 : IPermissionChecker
    {

        private readonly ICurrentUser _currentUser;
        private readonly ILogger<RemotePermissionChecker> _logger;
        private readonly IPermissionGrantAppService _permissionGrantAppService;
        private readonly IObjectMapper _mapper;

        public RemotePermissionChecker2(ICurrentUser currentUser,
            ILogger<RemotePermissionChecker> logger, 
            IPermissionGrantAppService permissionGrantAppService, 
            IObjectMapper mapper)
        {
            _currentUser = currentUser;
            _logger = logger;
            _permissionGrantAppService = permissionGrantAppService;
            _mapper = mapper;
        }

        public async Task<bool> IsGrantedAsync(string name)
        {
            if(_currentUser.IsAuthenticated == false)
                throw new System.Exception("You don't have sufficient authorization");

            return await _permissionGrantAppService.HasAccessAsync(_currentUser.Id.Value , name);
        }

        public async Task<bool> IsGrantedAsync(ClaimsPrincipal claimsPrincipal, string name)
        {
            var userId = claimsPrincipal.FindUserId()?.ToString();

            var user = _currentUser.Id.Value;
            var roles = _currentUser.FindClaimValue("role");
            //var roles = claimsPrincipal.FindFirst("role");

            if (userId is null)
                throw new System.Exception("You don't have sufficient authorization");

            var inputDto = new UserInfoForPermissionGrantInputDto()
            {
                UserId = new System.Guid(userId),
                Permission = name,
                Roles = new List<string> {"admin"}
            };

            return await _permissionGrantAppService.HasAccessAsync(inputDto);

        }

        public async Task<MultiplePermissionGrantResult> IsGrantedAsync(string[] names)
        {
            _logger.LogInformation($"Checking permission {string.Join(", ", names)} for {_currentUser.Id}");

            //var result = await _permissionAppService.CheckPermissionsAsync(new CheckPermissionsInput
            //{
            //    Names = names,
            //    Type = "User",
            //    Id = _currentUser.Id?.ToString(),
            //});

            return new MultiplePermissionGrantResult();
            //return await _permissionGrantAppService.HasAccessAsync(inputDto);

            //return _mapper.Map<MultiplePermissionGrantResultDto, MultiplePermissionGrantResult>(result);
        }

        public async Task<MultiplePermissionGrantResult> IsGrantedAsync(ClaimsPrincipal claimsPrincipal, string[] names)
        {
            return new MultiplePermissionGrantResult();
            
        }

        //public async Task<bool> IsGrantedAsync(ClaimsPrincipal claimsPrincipal, string name)
        //{
        //    var clientId = claimsPrincipal.FindClientId();
        //    var userId = claimsPrincipal.FindUserId()?.ToString();

        //    _logger.LogInformation($"Checking permission {name} for principal {userId}/{clientId}");

        //    return await _permissionAppService.CheckPermissionAsync(new CheckPermissionInput
        //    {
        //        Name = name,
        //        Type = userId == null && clientId != null ? "Client" : "User",
        //        Id = claimsPrincipal.FindUserId()?.ToString() ?? clientId,
        //    });
        //}

        //public async Task<bool> IsGrantedAsync(string name)
        //{
        //    _logger.LogInformation($"Checking permission {name} for principal {_currentUser.Id}");

        //    return await _permissionAppService.CheckPermissionAsync(new CheckPermissionInput
        //    {
        //        Name = name,
        //        Type = "User",
        //        Id = _currentUser.Id?.ToString(),
        //    });
        //}

        //public async Task<MultiplePermissionGrantResult> IsGrantedAsync(string[] names)
        //{
        //    _logger.LogInformation($"Checking permission {string.Join(", ", names)} for {_currentUser.Id}");

        //    var result = await _permissionAppService.CheckPermissionsAsync(new CheckPermissionsInput
        //    {
        //        Names = names,
        //        Type = "User",
        //        Id = _currentUser.Id?.ToString(),
        //    });

        //    return _mapper.Map<MultiplePermissionGrantResultDto, MultiplePermissionGrantResult>(result);
        //}

        //public async Task<MultiplePermissionGrantResult> IsGrantedAsync(ClaimsPrincipal claimsPrincipal, string[] names)
        //{
        //    var clientId = claimsPrincipal.FindClientId();
        //    var userId = claimsPrincipal.FindUserId()?.ToString();

        //    _logger.LogInformation($"Checking permission {string.Join(", ", names)} for principal {userId}/{clientId}");

        //    var result = await _permissionAppService.CheckPermissionsAsync(new CheckPermissionsInput
        //    {
        //        Names = names,
        //        Type = userId == null && clientId != null ? "Client" : "User",
        //        Id = claimsPrincipal.FindUserId()?.ToString() ?? clientId
        //    });

        //    return _mapper.Map<MultiplePermissionGrantResultDto, MultiplePermissionGrantResult>(result);
        //}
    }
}
