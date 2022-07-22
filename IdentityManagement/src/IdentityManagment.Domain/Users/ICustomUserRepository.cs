using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace IdentityManagment.Users
{
    public interface  ICustomUserRepository : IRepository<IdentityUser, Guid>
    {

        Task<bool> IsUserExist(Guid id, CancellationToken cancellationToken = default);
        
        Task<IdentityUser> GetUserWithRolesAsync(Guid id);
    }
}
