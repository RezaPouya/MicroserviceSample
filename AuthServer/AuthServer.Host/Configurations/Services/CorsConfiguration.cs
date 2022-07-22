namespace AuthServer.Host.Configurations.Services
{
    internal static class CorsConfiguration
    {
        internal static void ConfigureCors(this ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            var corsOrogingArray = configuration["App:CorsOrigins"]
               .Split(",", StringSplitOptions.RemoveEmptyEntries)
                   .Select(o => o.Trim().RemovePostFix("/")).ToArray();

            context.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                        .WithOrigins(corsOrogingArray)
                        .WithAbpExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }
    }
}