namespace IdentityService.Configurations
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