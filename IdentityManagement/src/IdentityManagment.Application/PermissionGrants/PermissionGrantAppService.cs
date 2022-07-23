using IdentityManagment.PermissionGrants.Dto;
using IdentityManagment.Roles;
using IdentityManagment.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.PermissionManagement;

namespace IdentityManagment.PermissionGrants
{
    public class PermissionGrantAppService : IdentityManagmentAppService, IPermissionGrantAppService
    {
        private readonly ICustomPermissionGrantRepository _customPermissionGrantRepository;
        private readonly ICustomRoleRepository _customRoleRepository;
        private readonly ICustomUserRepository _customUserRepository;

        public PermissionGrantAppService(
            ICustomPermissionGrantRepository customPermissionGrantRepository,
            ICustomRoleRepository customRoleRepository, 
            ICustomUserRepository customUserRepository)
        {
            _customPermissionGrantRepository = customPermissionGrantRepository;
            _customRoleRepository = customRoleRepository;
            _customUserRepository = customUserRepository;
        }

        public async Task<bool> HasAccessAsync(UserInfoForPermissionGrantInputDto inputModel)
        {
            if (inputModel is null)
                return false;

            if (inputModel.UserId == Guid.Empty)
                return false;

            if (string.IsNullOrEmpty(inputModel.Permission))
                return false;

            return await HasPermission(inputModel.UserId, inputModel.Permission, inputModel.Roles);
        }

        public async Task<bool> HasAccessAsync(Guid userId, string permission)
        {
            if (userId == Guid.Empty)
                return false;

            if (string.IsNullOrEmpty(permission))
                return false;

            var user = await _customUserRepository.GetAsync(userId, default);

            if (user is null)
                return false;

            var roles = await _customRoleRepository.GetUserRolesNames(user, default);

            if (roles is null || roles.Any() == false)
                return false;

            return await HasPermission(userId, permission, roles);
        }

        private async Task<bool> HasPermission(Guid userId, string permission, IEnumerable<string> roles)
        {

            var providers = GetAllProviderNames(roles, userId);
            var permissionGrants = await _customPermissionGrantRepository
                           .GetAllAsync(providers, default);

            return permissionGrants.Any(p => p.Name.Equals(permission.Trim()));
        }

        private static List<string> GetAllProviderNames(IEnumerable<string> roles, Guid userId)
        {
            var providerNames = roles.ToList();
            providerNames.Add(userId.ToString());
            return providerNames;
        }

    }
}
