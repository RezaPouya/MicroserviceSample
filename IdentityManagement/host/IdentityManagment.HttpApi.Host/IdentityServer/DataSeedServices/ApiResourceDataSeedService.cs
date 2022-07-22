using IdentityManagment.Definitions;
using System.Collections.Generic;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.IdentityServer.ApiResources;
using Volo.Abp.Uow;

namespace IdentityManagment.IdentityServer.DataSeedServices
{
    [UnitOfWork]
    public class ApiResourceDataSeedService : ITransientDependency
    {
        private readonly IApiResourceRepository _apiResourceRepository;
        private readonly IGuidGenerator _guidGenerator;

        public ApiResourceDataSeedService(IApiResourceRepository apiResourceRepository, IGuidGenerator guidGenerator)
        {
            this._apiResourceRepository = apiResourceRepository;
            this._guidGenerator = guidGenerator;
        }

        [UnitOfWork]
        public virtual async Task SeedAsync()
        {
            var apiResources = new List<string>()
            {
                "identity_management",
                "identity_service",
                "identity_user"
            };

            var commonApiUserClaims = new[]
            {
                "email",
                "email_verified",
                "name",
                "phone_number",
                "phone_number_verified",
                "role"
            };

            foreach (var apiResourceName in MicroserviceApiResource.GetAll())
            {
                await CreateApiResourceAsync(apiResourceName, commonApiUserClaims);
            }
        }

        private async Task<ApiResource> CreateApiResourceAsync(string apiResourceName, IEnumerable<string> claims)
        {
            var apiResource = await _apiResourceRepository.FindByNameAsync(apiResourceName);

            if (apiResource == null)
            {
                var aggregateRoot = new ApiResource(_guidGenerator.Create(), apiResourceName, displayName: apiResourceName + " API");

                await _apiResourceRepository.InsertAsync(aggregateRoot, autoSave: true);
            }

            var entity = await _apiResourceRepository.FindByNameAsync(apiResourceName);

            foreach (var claim in claims)
            {
                if (entity.FindClaim(claim) == null)
                {
                    entity.AddUserClaim(claim);
                }
            }

            return await _apiResourceRepository.UpdateAsync(entity);
        }
    }
}