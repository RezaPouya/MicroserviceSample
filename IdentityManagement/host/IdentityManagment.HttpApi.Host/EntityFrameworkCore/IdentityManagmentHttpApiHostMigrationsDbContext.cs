using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace IdentityManagment.EntityFrameworkCore;

public class IdentityManagmentHttpApiHostMigrationsDbContext : AbpDbContext<IdentityManagmentHttpApiHostMigrationsDbContext>
{
    public IdentityManagmentHttpApiHostMigrationsDbContext(DbContextOptions<IdentityManagmentHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureIdentityManagment();
    }
}
