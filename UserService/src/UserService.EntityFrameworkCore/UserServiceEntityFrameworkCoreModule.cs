using Microsoft.Extensions.DependencyInjection;
using UserService.EntityFrameworkCore;
using UserService.Repositories;
using UserService.Users.Repositories;
using Volo.Abp.Dapper;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace UserService
{
    [DependsOn(typeof(UserServiceDomainModule))]
    [DependsOn(typeof(AbpEntityFrameworkCoreModule))]
    [DependsOn(typeof(AbpDapperModule))]
    public class UserServiceEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<UserServiceDbContext>(options =>
            {
                options.Services.AddTransient<IUserRepository, UserRepository>();
            });
        }
    }
}