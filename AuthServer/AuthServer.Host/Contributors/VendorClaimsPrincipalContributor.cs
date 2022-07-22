using System.Security.Claims;
using System.Security.Principal;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Security.Claims;

namespace AuthServer.Host.Contributors
{
    public class VendorClaimsPrincipalContributor : IAbpClaimsPrincipalContributor, ITransientDependency
    {
        //private ICustomRoleRepository _customRoleRepository;
        //private ICustomUserRepository _customUserRepository;
        //private IAuthVendorAppService _authVendorAppService;

        public async Task ContributeAsync(AbpClaimsPrincipalContributorContext context)
        {
            var identity = context.ClaimsPrincipal.Identities.FirstOrDefault();

            var userId = identity?.FindUserId();

            if (userId is null)
                return;

            ResolveServices(context);

            //var user = await _customUserRepository.GetUserWithRolesAsync(userId.Value);

            //var roles = await _customRoleRepository.GetUserRolesNames(user);

            //if (roles.Contains(RoleConstants.Vendor) == false)
            //    return;

            //var userAuthModel =
            //    await _authVendorAppService.GetUserVendors(user.Id, default).ConfigureAwait(false);

            //if (userAuthModel?.VendorUsers is null || userAuthModel.VendorUsers.Any() == false)
            //    return;

            //var firstActiveUserVendor = userAuthModel.VendorUsers
            //    .Where(p => p.IsActive)
            //    .FirstOrDefault(p => p.UserIsActive);

            //if (firstActiveUserVendor is null)
            //    return;

            //var vendorIdClaim = new Claim(ClaimConstants.VendorId, firstActiveUserVendor.VendorId.ToString());
            //var vendorCodeClaim = new Claim(ClaimConstants.VendorCode, firstActiveUserVendor.VendorCode);
            //var vendorNameClaim = new Claim(ClaimConstants.VendorName, firstActiveUserVendor.Name);

            //identity.AddClaim(vendorIdClaim);
            //identity.AddClaim(vendorCodeClaim);
            //identity.AddClaim(vendorNameClaim);
        }

        private void ResolveServices(AbpClaimsPrincipalContributorContext context)
        {
            //_customUserRepository = context.ServiceProvider.GetRequiredService<ICustomUserRepository>();
            //_customRoleRepository = context.ServiceProvider.GetRequiredService<ICustomRoleRepository>();
            //_authVendorAppService = context.ServiceProvider.GetRequiredService<IAuthVendorAppService>();
        }
    }
}