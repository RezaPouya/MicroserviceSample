using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using Volo.Abp.Modularity;

namespace AuthServer.Host.Configurations.Services
{
    internal static class RedisConfiguration
    {
        internal static void ConfigureRedis(this ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();

            var dataProtectionBuilder = context
                .Services.AddDataProtection()
                .SetApplicationName(AuthServerHostConstants.ApplicationName);

            if (!hostingEnvironment.IsDevelopment())
            {
                var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
                var redisProtectionKey = $"{AuthServerHostConstants.ApplicationName}-Protection-Keys";
                dataProtectionBuilder.PersistKeysToStackExchangeRedis(redis, redisProtectionKey);
            }
        }
    }
}