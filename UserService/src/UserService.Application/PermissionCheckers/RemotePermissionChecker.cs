﻿using Volo.Abp.ObjectMapping;
using IdentityManagment.PermissionCheckers;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Security.Principal;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Users;
using System.Threading.Tasks;

namespace UserService.PermissionCheckers
{
    public class RemotePermissionChecker : IPermissionChecker
    {

        private readonly ICurrentUser _currentUser;
        private readonly ILogger<RemotePermissionChecker> _logger;
        private readonly IPermissionCheckerAppService _permissionAppService;
        private readonly IObjectMapper _mapper;

        public RemotePermissionChecker(
            ICurrentUser currentUser,
            ILogger<RemotePermissionChecker> logger,
            IPermissionCheckerAppService permissionAppService,
            IObjectMapper mapper)
        {
            _currentUser = currentUser;
            _logger = logger;
            _permissionAppService = permissionAppService;
            _mapper = mapper;
        }

        public async Task<bool> IsGrantedAsync(ClaimsPrincipal claimsPrincipal, string name)
        {
            var clientId = claimsPrincipal.FindClientId();
            var userId = claimsPrincipal.FindUserId()?.ToString();

            _logger.LogInformation($"Checking permission {name} for principal {userId}/{clientId}");

            return await _permissionAppService.CheckPermissionAsync(new CheckPermissionInput
            {
                Name = name,
                Type = userId == null && clientId != null ? "Client" : "User",
                Id = claimsPrincipal.FindUserId()?.ToString() ?? clientId,
            });
        }

        public async Task<bool> IsGrantedAsync(string name)
        {
            _logger.LogInformation($"Checking permission {name} for principal {_currentUser.Id}");

            return await _permissionAppService.CheckPermissionAsync(new CheckPermissionInput
            {
                Name = name,
                Type = "User",
                Id = _currentUser.Id?.ToString(),
            });
        }

        public async Task<MultiplePermissionGrantResult> IsGrantedAsync(string[] names)
        {
            _logger.LogInformation($"Checking permission {string.Join(", ", names)} for {_currentUser.Id}");

            var result = await _permissionAppService.CheckPermissionsAsync(new CheckPermissionsInput
            {
                Names = names,
                Type = "User",
                Id = _currentUser.Id?.ToString(),
            });

            return _mapper.Map<MultiplePermissionGrantResultDto, MultiplePermissionGrantResult>(result);
        }

        public async Task<MultiplePermissionGrantResult> IsGrantedAsync(ClaimsPrincipal claimsPrincipal, string[] names)
        {
            var clientId = claimsPrincipal.FindClientId();
            var userId = claimsPrincipal.FindUserId()?.ToString();

            _logger.LogInformation($"Checking permission {string.Join(", ", names)} for principal {userId}/{clientId}");

            var result = await _permissionAppService.CheckPermissionsAsync(new CheckPermissionsInput
            {
                Names = names,
                Type = userId == null && clientId != null ? "Client" : "User",
                Id = claimsPrincipal.FindUserId()?.ToString() ?? clientId
            });

            return _mapper.Map<MultiplePermissionGrantResultDto, MultiplePermissionGrantResult>(result);
        }
    }
}
