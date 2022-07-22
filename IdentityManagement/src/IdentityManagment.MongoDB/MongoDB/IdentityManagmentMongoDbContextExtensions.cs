using Volo.Abp;
using Volo.Abp.MongoDB;

namespace IdentityManagment.MongoDB;

public static class IdentityManagmentMongoDbContextExtensions
{
    public static void ConfigureIdentityManagment(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
