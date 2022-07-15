using Volo.Abp;
using Volo.Abp.MongoDB;

namespace IdentityService.MongoDB;

public static class IdentityServiceMongoDbContextExtensions
{
    public static void ConfigureIdentityService(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
