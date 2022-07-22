using IdentityManagment.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace IdentityManagment;

public abstract class IdentityManagmentController : AbpControllerBase
{
    protected IdentityManagmentController()
    {
        LocalizationResource = typeof(IdentityManagmentResource);
    }
}
