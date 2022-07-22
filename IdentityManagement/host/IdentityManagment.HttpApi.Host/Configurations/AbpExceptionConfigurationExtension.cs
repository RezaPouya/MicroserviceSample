using IdentityManagment.Extensions;
using Volo.Abp.AspNetCore.ExceptionHandling;

namespace IdentityManagment.Configurations
{
    public static class AbpExceptionConfigurationExtension
    {
        public static void ConfigureAbpException(this ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            if (hostingEnvironment.IsDevelopment())
            {
                context.Configure<AbpExceptionHandlingOptions>(options =>
                {
                    options.SendExceptionsDetailsToClients = true;
                    options.SendStackTraceToClients = true;
                });
            }
        }
    }
}