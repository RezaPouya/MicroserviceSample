using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace UserService.EntityFrameworkCore;

public class UserServiceHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<UserServiceHttpApiHostMigrationsDbContext>
{
    public UserServiceHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<UserServiceHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("UserService"));

        return new UserServiceHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
