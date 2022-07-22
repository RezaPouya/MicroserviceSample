using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace IdentityManagment.Configurations
{
    public static class SwaggerConfigurationExtension
    {
        public static void ConfigureSwagger(this ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            var configuration = context.Services.GetConfiguration();

            var allowedScopes = new Dictionary<string, string>() {
                {"user_service", "User Service API"},
                {"gateway" , "Gateway" },
                {"identity_service" , "Identity Service API" },
                {"identity_management" , "Identity Management API" }
            };

            var authority = configuration["AuthServer:Authority"].ToString();

            context.Services.AddAbpSwaggerGenWithOAuth(authority,
                allowedScopes,
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "identity management API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                    options.CustomSchemaIds(type => type.FullName);
                });
        }
    }
}