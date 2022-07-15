using UserService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace UserService;

public abstract class UserServiceController : AbpControllerBase
{
    protected UserServiceController()
    {
        LocalizationResource = typeof(UserServiceResource);
    }
}
