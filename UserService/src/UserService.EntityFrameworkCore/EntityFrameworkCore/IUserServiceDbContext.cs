using Microsoft.EntityFrameworkCore;
using UserService.Users;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace UserService.EntityFrameworkCore;

[ConnectionStringName(UserServiceDbProperties.ConnectionStringName)]
public interface IUserServiceDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:*/

    DbSet<User> Users { get; }
}
