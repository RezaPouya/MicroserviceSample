using Microsoft.Extensions.Configuration;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.RabbitMQ;

namespace UserService.Configurations
{
    public static class RabbitMqConfigurationExtension
    {
        public static void ConfigureRabbitMq(this ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            RabbitMqConfigurationModel options = configuration.GetSection("RabbitMq").Get<RabbitMqConfigurationModel>();

            if (options.IsEnabled == false)
            {
                return;
            }

            context.Configure<AbpRabbitMqOptions>(opts =>
            {
                opts.Connections.Default.HostName = options.Connection.HostName;
                opts.Connections.Default.Port = options.Connection.Port ?? 0;
                opts.Connections.Default.UserName = options.Connection.UserName;
                opts.Connections.Default.Password = options.Connection.Password;
            });

            context.Configure<AbpRabbitMqEventBusOptions>(opts =>
            {
                opts.ClientName = options.EventBus.ClientName;
                opts.ExchangeName = options.EventBus.ExchangeName;
            });
        }
    }
}