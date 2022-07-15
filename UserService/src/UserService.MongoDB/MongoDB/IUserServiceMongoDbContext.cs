using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace UserService.MongoDB;

[ConnectionStringName(UserServiceDbProperties.ConnectionStringName)]
public interface IUserServiceMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}
