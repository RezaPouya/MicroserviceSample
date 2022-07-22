using IdentityManagment.PermissionGrants.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace IdentityManagment.PermissionGrants
{
    public interface IPermissionGrantAppService : IApplicationService
    {
        Task<bool> HasAccessAsync(UserInfoForPermissionGrantInputDto inputModel);

        Task<bool> HasAccessAsync(Guid userId , string permission);
    }
}
