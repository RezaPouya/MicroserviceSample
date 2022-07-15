using Microsoft.EntityFrameworkCore;
using UserService.Users;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace UserService.EntityFrameworkCore;

[ConnectionStringName(UserServiceDbProperties.ConnectionStringName)]
public class UserServiceDbContext : AbpDbContext<UserServiceDbContext>, IUserServiceDbContext
{
    public DbSet<User> Users { get; set; }

    public UserServiceDbContext(DbContextOptions<UserServiceDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureUserService();
    }
}
