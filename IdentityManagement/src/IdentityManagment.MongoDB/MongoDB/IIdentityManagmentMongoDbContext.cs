using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace IdentityManagment.MongoDB;

[ConnectionStringName(IdentityManagmentDbProperties.ConnectionStringName)]
public interface IIdentityManagmentMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}
