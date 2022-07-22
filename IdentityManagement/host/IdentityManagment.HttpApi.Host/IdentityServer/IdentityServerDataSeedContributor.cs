using IdentityManagment.IdentityServer.DataSeedServices;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace IdentityManagment.IdentityServer
{
    public class IdentityServerDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly CustomIdentityResourceDataSeedService _customIdentityResourceDataSeedService;
        private readonly ApiResourceDataSeedService _apiResourceDataSeedService;
        private readonly ApiScopeDataSeedService _apiScopeDataSeedService;
        private readonly ClientDataSeedService _clientDataSeedService;
        private readonly AdminPermissionDataSeedService _adminPermissionDataSeedService;
        private readonly UserDataSeedService _userDataSeedService;
        private readonly RoleDataSeedService _roleDataSeedService;

        public IdentityServerDataSeedContributor(CustomIdentityResourceDataSeedService customIdentityResourceDataSeedService,
            ApiResourceDataSeedService apiResourceDataSeedService, ApiScopeDataSeedService apiScopeDataSeedService,
            ClientDataSeedService clientDataSeedService, AdminPermissionDataSeedService adminPermissionDataSeedService, 
            UserDataSeedService userDataSeedService, RoleDataSeedService roleDataSeedService)
        {
            _customIdentityResourceDataSeedService = customIdentityResourceDataSeedService;
            _apiResourceDataSeedService = apiResourceDataSeedService;
            _apiScopeDataSeedService = apiScopeDataSeedService;
            _clientDataSeedService = clientDataSeedService;
            _adminPermissionDataSeedService = adminPermissionDataSeedService;
            _userDataSeedService = userDataSeedService;
            _roleDataSeedService = roleDataSeedService;
        }

        [UnitOfWork]
        public virtual async Task SeedAsync(DataSeedContext context)
        {
            await _customIdentityResourceDataSeedService.CreateStandardResourcesAsync();

            await _roleDataSeedService.SeedAsync();

            await _userDataSeedService.SeedAsync();

            await _adminPermissionDataSeedService.SeedAsync();

            await _apiScopeDataSeedService.SeedAsync();

            await _apiResourceDataSeedService.SeedAsync();

            await _clientDataSeedService.SeedAsync();
        }
    }
}