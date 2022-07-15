using Microsoft.AspNetCore.Cors;

namespace UserService.Configurations
{
    public static class CorsConfigurationExtension
    {
        public static void ConfigureCors(this ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            var corsOrigins = configuration["App:CorsOrigins"]
                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                .Select(o => o.Trim().RemovePostFix("/"))
                .ToArray();

            context.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins(corsOrigins)
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