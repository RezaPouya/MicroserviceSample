using IdentityManagment.Constants;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer.IdentityResources;
using Volo.Abp.Uow;

namespace IdentityManagment.IdentityServer.DataSeedServices
{
    [UnitOfWork]
    public class CustomIdentityResourceDataSeedService : /*IIdentityResourceDataSeeder,*/ ITransientDependency
    {
        protected IIdentityClaimTypeRepository ClaimTypeRepository { get; }
        protected IIdentityResourceRepository IdentityResourceRepository { get; }
        protected IGuidGenerator GuidGenerator { get; }

        public CustomIdentityResourceDataSeedService(
            IIdentityResourceRepository identityResourceRepository,
            IGuidGenerator guidGenerator,
            IIdentityClaimTypeRepository claimTypeRepository)
        {
            IdentityResourceRepository = identityResourceRepository;
            GuidGenerator = guidGenerator;
            ClaimTypeRepository = claimTypeRepository;
        }

        [UnitOfWork]
        public virtual async Task CreateStandardResourcesAsync()
        {
            var resources = new[]
            {
                new IdentityServer4.Models.IdentityResources.OpenId(),
                new IdentityServer4.Models.IdentityResources.Profile(),
                new IdentityServer4.Models.IdentityResources.Email(),
                new IdentityServer4.Models.IdentityResources.Address(),
                new IdentityServer4.Models.IdentityResources.Phone(),
                //new IdentityServer4.Models.IdentityResource("role", "Roles of the user", new[] {"role"}),
                new IdentityServer4.Models.IdentityResource(ClaimTypeConstants.Role, "Roles of the user",
                new[] {ClaimConstants.Role}),
                new IdentityServer4.Models.IdentityResource(ClaimTypeConstants.permission , "Permission of the user " ,
                new [] {ClaimConstants.Permissions ,})
            };

            foreach (var resource in resources)
            {
                foreach (var claimType in resource.UserClaims)
                {
                    await AddClaimTypeIfNotExistsAsync(claimType);
                }

                await AddIdentityResourceIfNotExistsAsync(resource);
            }
        }

        protected virtual async Task AddIdentityResourceIfNotExistsAsync(IdentityServer4.Models.IdentityResource resource)
        {
            if (await IdentityResourceRepository.CheckNameExistAsync(resource.Name))
            {
                return;
            }

            await IdentityResourceRepository.InsertAsync(
                new IdentityResource(
                    GuidGenerator.Create(),
                    resource
                )
            );
        }

        protected virtual async Task AddClaimTypeIfNotExistsAsync(string claimType)
        {
            if (await ClaimTypeRepository.AnyAsync(claimType))
            {
                return;
            }

            await ClaimTypeRepository.InsertAsync(
                new IdentityClaimType(
                    GuidGenerator.Create(),
                    claimType,
                    isStatic: true
                )
            );
        }
    }
}


