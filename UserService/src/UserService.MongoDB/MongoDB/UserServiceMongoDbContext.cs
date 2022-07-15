using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace UserService.MongoDB;

[ConnectionStringName(UserServiceDbProperties.ConnectionStringName)]
public class UserServiceMongoDbContext : AbpMongoDbContext, IUserServiceMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureUserService();
    }
}
