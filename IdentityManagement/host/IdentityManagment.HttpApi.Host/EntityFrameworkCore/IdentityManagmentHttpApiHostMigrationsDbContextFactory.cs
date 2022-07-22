using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace IdentityManagment.EntityFrameworkCore;

public class IdentityManagmentHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<IdentityManagmentHttpApiHostMigrationsDbContext>
{
    public IdentityManagmentHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<IdentityManagmentHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("IdentityManagment"));

        return new IdentityManagmentHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
