using Volo.Abp;
using Volo.Abp.MongoDB;

namespace UserService.MongoDB;

public static class UserServiceMongoDbContextExtensions
{
    public static void ConfigureUserService(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
