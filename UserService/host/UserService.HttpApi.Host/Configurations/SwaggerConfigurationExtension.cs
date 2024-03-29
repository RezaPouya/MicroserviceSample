﻿using Microsoft.OpenApi.Models;

namespace UserService.Configurations
{
    public static class SwaggerConfigurationExtension
    {
        public static void ConfigureSwagger(this ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            var configuration = context.Services.GetConfiguration();

            var allowedScopes = new Dictionary<string, string>() {
                {"user_service", "UserService API"},
                {"internal_service_gateway" , "Internal Gateway" },
                
            };

            var authority = configuration["AuthServer:Authority"].ToString();

            context.Services.AddAbpSwaggerGenWithOAuth(authority,
                allowedScopes,
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "UserService API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                    options.CustomSchemaIds(type => type.FullName);
                });
        }
    }
}