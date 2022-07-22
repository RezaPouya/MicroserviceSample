
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace AuthServer.Host
{
    [Dependency(ReplaceServices = true)]
    public class AuthServerBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => AuthServerHostConstants.ApplicationName;
    }
}