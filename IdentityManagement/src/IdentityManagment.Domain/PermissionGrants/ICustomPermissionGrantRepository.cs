using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.PermissionManagement;

namespace IdentityManagment.PermissionGrants
{
    public interface ICustomPermissionGrantRepository : IRepository<PermissionGrant, Guid>
    {
        Task<IEnumerable<PermissionGrant>> GetAllAsync(string providername, CancellationToken cancellationToken);
        Task<IEnumerable<PermissionGrant>> GetAllAsync(IEnumerable<string> providerNames, CancellationToken cancellationToken);

        Task<bool> IsExistAsync(string providername, CancellationToken cancellationToken);
        Task<bool> IsExistAsync(IEnumerable<string> providerNames, CancellationToken cancellationToken);
    }
}
