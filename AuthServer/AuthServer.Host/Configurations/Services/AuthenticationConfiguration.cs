using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using Volo.Abp.Modularity;

namespace AuthServer.Host.Configurations.Services
{
    internal static class AuthenticationConfiguration
    {
        internal static void ConfigureAuthentication(this ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            var authority = configuration["AuthServer:Authority"];
            var requireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);

            context.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                //opt.Audience = "AuthServer"; // apiname
                opt.Authority = authority;
                opt.RequireHttpsMetadata = requireHttpsMetadata;
                opt.TokenValidationParameters = new()
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                };
            });
        }
    }
}