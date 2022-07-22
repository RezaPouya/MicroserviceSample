using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace IdentityManagment.Roles
{
    public interface ICustomRoleRepository : IRepository<IdentityRole, Guid>
    {
        public Task<IEnumerable<string>> GetUserRolesNames(IdentityUser user, CancellationToken cancellationToken = default);
    }
}