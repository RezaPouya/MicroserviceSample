using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace IdentityManagment.MongoDB;

[ConnectionStringName(IdentityManagmentDbProperties.ConnectionStringName)]
public class IdentityManagmentMongoDbContext : AbpMongoDbContext, IIdentityManagmentMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureIdentityManagment();
    }
}
