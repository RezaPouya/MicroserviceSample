using Microsoft.AspNetCore.Http;

namespace AuthServer.Host
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<AuthServerModule>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });
            app.InitializeApplication();
        }
    }
}