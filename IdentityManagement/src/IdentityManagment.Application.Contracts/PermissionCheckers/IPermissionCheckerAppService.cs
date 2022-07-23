using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace IdentityManagment.PermissionCheckers
{
    public interface IPermissionCheckerAppService : IApplicationService
    {
        Task<bool> CheckPermissionAsync(CheckPermissionInput input);

        Task<MultiplePermissionGrantResultDto> CheckPermissionsAsync(CheckPermissionsInput input);
    }
}
