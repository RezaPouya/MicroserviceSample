using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace UserService.EntityFrameworkCore;

public class UserServiceHttpApiHostMigrationsDbContext : AbpDbContext<UserServiceHttpApiHostMigrationsDbContext>
{
    public UserServiceHttpApiHostMigrationsDbContext(DbContextOptions<UserServiceHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureUserService();
    }
}
