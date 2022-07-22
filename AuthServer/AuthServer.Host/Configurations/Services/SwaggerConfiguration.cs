using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using Volo.Abp.Modularity;

namespace AuthServer.Host.Configurations.Services
{
    internal static class SwaggerConfiguration
    {
        internal static void ConfigureSwagger(this ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();
            var authority = configuration["AuthServer:Authority"];

            var scopes = new Dictionary<string, string>();

            scopes.Add(AuthServerHostConstants.ApiName, "Auth Server API");

            context.Services.AddAbpSwaggerGenWithOAuth(authority,
                scopes,
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth Server", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                    options.CustomSchemaIds(type => type.FullName);
                });
        }
    }
}