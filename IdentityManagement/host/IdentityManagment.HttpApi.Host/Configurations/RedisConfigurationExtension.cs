using IdentityManagment.Extensions;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Configuration;
using Volo.Abp.Caching;

namespace IdentityManagment.Configurations
{
    /// <summary>
    /// https://imperugo.gitbook.io/stackexchange-redis-extensions/configuration/json-configuration
    /// https://stackexchange.github.io/StackExchange.Redis/Configuration.html
    /// </summary>
    public static class RedisConfigurationExtension
    {
        public static void ConfigureRedis(this ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            var configuration = context.Services.GetConfiguration();

            var applicationName = configuration["App:ApplicationName"];

            context.Configure<AbpDistributedCacheOptions>(options => { options.KeyPrefix = $"{applicationName}:"; });

            bool isEnabled = Convert.ToBoolean(configuration["Redis:IsEnabled"]);

            if (isEnabled == false)
            {
                return;
            }

            var redisConfiguration = configuration.GetSection("Redis").Get<RedisConfiguration>();

            var dataProtectionBuilder = context.Services.AddDataProtection().SetApplicationName(applicationName);

            if (!hostingEnvironment.IsDevelopment())
            {
                //var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);

                var redis = ConnectionMultiplexer.Connect(redisConfiguration.ConfigurationOptions);

                var protectionKeys = $"{applicationName}-Protection-Keys";

                dataProtectionBuilder.PersistKeysToStackExchangeRedis(redis, protectionKeys);
            }
        }
    }
}