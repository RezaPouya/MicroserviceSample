using IdentityService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace IdentityService;

public abstract class IdentityServiceController : AbpControllerBase
{
    protected IdentityServiceController()
    {
        LocalizationResource = typeof(IdentityServiceResource);
    }
}
