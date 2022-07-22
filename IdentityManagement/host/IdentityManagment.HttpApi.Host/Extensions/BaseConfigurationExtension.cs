namespace IdentityManagment.Extensions
{
    public static class BaseConfigurationExtension
    {
        public static void Configure<TOptions>(this ServiceConfigurationContext context, Action<TOptions> configureOptions) where TOptions : class
        {
            context.Services.Configure(configureOptions);
        }
    }
}