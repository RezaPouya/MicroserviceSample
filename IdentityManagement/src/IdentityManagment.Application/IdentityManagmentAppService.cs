using IdentityManagment.Localization;
using Volo.Abp.Application.Services;

namespace IdentityManagment;

public abstract class IdentityManagmentAppService : ApplicationService
{
    protected IdentityManagmentAppService()
    {
        LocalizationResource = typeof(IdentityManagmentResource);
        ObjectMapperContext = typeof(IdentityManagmentApplicationModule);
    }
}
