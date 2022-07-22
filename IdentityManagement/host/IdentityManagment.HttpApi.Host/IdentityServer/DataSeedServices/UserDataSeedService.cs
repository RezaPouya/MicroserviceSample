using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.Uow;

namespace IdentityManagment.IdentityServer.DataSeedServices
{
    [UnitOfWork]
    public class UserDataSeedService : ITransientDependency
    {
        private readonly IdentityUserManager _identityUserManager;
        private readonly IdentityRoleManager _roleManager;
        private readonly IGuidGenerator _guidGenerator;

        public UserDataSeedService(IdentityUserManager identityUserManager,
            IdentityRoleManager roleManager,
            IGuidGenerator guidGenerator)
        {
            _identityUserManager = identityUserManager;
            _roleManager = roleManager;
            _guidGenerator = guidGenerator;
        }

        //[UnitOfWork]
        public virtual async Task SeedAsync()
        {
            const string eamil = "admin@abp.io";
            var admin = await _identityUserManager.FindByEmailAsync(eamil);

            if (admin is not null)
            {
                await _identityUserManager.AddToRoleAsync(admin, "admin");
                return;
            }

            var adminUser = new IdentityUser(_guidGenerator.Create(), "Admin", eamil);

            await _identityUserManager.CreateAsync(adminUser);

            admin = await _identityUserManager.FindByEmailAsync(eamil);

            await _identityUserManager.AddPasswordAsync(admin, "1q2w3E*");

            await _identityUserManager.AddToRoleAsync(admin, "admin");
        }
    }
}