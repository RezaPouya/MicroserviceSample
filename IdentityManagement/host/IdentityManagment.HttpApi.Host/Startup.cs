using Microsoft.AspNetCore.Http;

namespace IdentityManagment
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<IdentityManagmentHttpApiHostModule>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });
            app.InitializeApplication();
        }
    }
}