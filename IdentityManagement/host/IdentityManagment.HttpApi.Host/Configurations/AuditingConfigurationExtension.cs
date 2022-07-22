using IdentityManagment.Extensions;
using Microsoft.Extensions.Configuration;
using Volo.Abp.Auditing;

namespace IdentityManagment.Configurations
{

    public static class AuditingConfigurationExtension
    {
        public static void ConfigureAuditing(this ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            AbpAuditingOptions options = configuration.GetSection("Auditing").Get<AbpAuditingOptions>();

            options.ApplicationName = configuration["App:ApplicationName"];

            context.Configure<AbpAuditingOptions>(opts =>
            {
                opts.IsEnabled = options.IsEnabled;
                opts.IsEnabledForGetRequests = options.IsEnabledForGetRequests;
                opts.ApplicationName = options.ApplicationName;
                opts.HideErrors = options.HideErrors;
                opts.IsEnabledForAnonymousUsers = options.IsEnabledForAnonymousUsers;
                opts.AlwaysLogOnException = options.AlwaysLogOnException;
            });
        }
    }
}