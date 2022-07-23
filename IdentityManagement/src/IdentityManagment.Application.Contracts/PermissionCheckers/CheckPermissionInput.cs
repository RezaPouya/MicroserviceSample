using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Authorization.Permissions;

namespace IdentityManagment.PermissionCheckers
{
    [Serializable]
    public class CheckPermissionInput : EntityDto
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
    }

    [Serializable]
    public class CheckPermissionsInput : EntityDto
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public ICollection<string> Names { get; set; }
    }


    [Serializable]
    public class MultiplePermissionGrantResultDto
    {
        public Dictionary<string, PermissionGrantResult> Result { get; set; }
    }
}
