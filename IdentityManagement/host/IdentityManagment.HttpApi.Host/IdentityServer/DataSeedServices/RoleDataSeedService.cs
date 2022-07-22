using IdentityManagment.Constants;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.Uow;

namespace IdentityManagment.IdentityServer.DataSeedServices
{
    [UnitOfWork]
    public class RoleDataSeedService : ITransientDependency
    {
        private readonly IdentityUserManager _identityUserManager;
        private readonly IdentityRoleManager _roleManager;
        private readonly IGuidGenerator _guidGenerator;

        public RoleDataSeedService(IdentityRoleManager identityRoleManager,
            IGuidGenerator guidGenerator,
            IdentityUserManager identityUserManager)
        {
            _roleManager = identityRoleManager;
            _guidGenerator = guidGenerator;
            _identityUserManager = identityUserManager;
        }

        [UnitOfWork]
        public virtual async Task SeedAsync()
        {
            foreach (var role in RoleConstants.GetAll())
            {
                var roleInDb = await _roleManager.FindByNameAsync(role);

                if (roleInDb is not null)
                    continue;

                var newRole = new IdentityRole(_guidGenerator.Create(), role);

                await _roleManager.CreateAsync(newRole);
            }
        }
    }
}