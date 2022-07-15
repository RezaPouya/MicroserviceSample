using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace UserService.EntityFrameworkCore;

public static class UserServiceDbContextModelCreatingExtensions
{
    public static void ConfigureUserService( this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.ApplyConfigurationsFromAssembly(typeof(UserServiceDbContext).Assembly);

    }
}
