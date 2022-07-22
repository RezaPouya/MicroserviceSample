using IdentityManagment.Definitions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.IdentityServer.ApiScopes;
using Volo.Abp.Uow;

namespace IdentityManagment.IdentityServer.DataSeedServices
{
    [UnitOfWork]
    public class ApiScopeDataSeedService : ITransientDependency
    {
        private readonly IApiScopeRepository _apiScopeRepository;
        private readonly IGuidGenerator _guidGenerator;

        public ApiScopeDataSeedService(IApiScopeRepository apiScopeRepository, IGuidGenerator guidGenerator)
        {
            this._apiScopeRepository = apiScopeRepository;
            this._guidGenerator = guidGenerator;
        }

        [UnitOfWork]
        public virtual async Task SeedAsync()
        {
            foreach (var scope in MicroserviceApiScopes.GetAll())
            {
                await CreateApiScopeAsync(scope);
            }
        }

        private async Task<ApiScope> CreateApiScopeAsync(string scope)
        {
            var apiScope = await _apiScopeRepository.FindByNameAsync(scope);

            if (apiScope == null)
            {
                var apiS = new ApiScope(_guidGenerator.Create(), scope, displayName: scope + " API");
                apiScope = await _apiScopeRepository.InsertAsync(apiS, autoSave: true);
            }

            return apiScope;
        }
    }
}