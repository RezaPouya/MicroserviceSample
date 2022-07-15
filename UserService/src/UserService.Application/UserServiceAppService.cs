using UserService.Localization;
using Volo.Abp.Application.Services;

namespace UserService;

public abstract class UserServiceAppService : ApplicationService
{
    protected UserServiceAppService()
    {
        LocalizationResource = typeof(UserServiceResource);
        ObjectMapperContext = typeof(UserServiceApplicationModule);
    }
}
