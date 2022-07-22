
using IdentityManagment.Extensions;

namespace IdentityManagment.Configurations
{
    public static class UseSqlServerConfigurationExtension
    {
        public static void ConfigureUseSqlServer(this ServiceConfigurationContext context)
        {
            context.Configure<AbpDbContextOptions>(options => { options.UseSqlServer(); });
        }
    }
}