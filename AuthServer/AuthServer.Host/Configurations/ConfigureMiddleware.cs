using AuthServer.Shared.Consants;
using Microsoft.AspNetCore.Localization;

namespace AuthServer.Host.Configurations
{
    internal static class ConfigureMiddleware
    {
        internal static void ConfigureMiddlewares(this ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseErrorPage();
                app.UseHsts();
            }

            app.UseAbpRequestLocalization(opt =>
            {
                //var faCultureInfo = new CultureInfo(AuthServerHostConstants.DefaultCultureName);
                //opt.DefaultRequestCulture = new RequestCulture(faCultureInfo);
                //opt.SupportedCultures = new List<CultureInfo> { faCultureInfo };
                //opt.SupportedUICultures = new List<CultureInfo> { faCultureInfo };
            });

            app.UseCorrelationId();
            app.UseStaticFiles();
            app.UseCors(AuthServerHostConstants.DefaultCorsPolicyName);
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseUnitOfWork();
            app.UseAuthorization();
            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints();
        }
    }
}