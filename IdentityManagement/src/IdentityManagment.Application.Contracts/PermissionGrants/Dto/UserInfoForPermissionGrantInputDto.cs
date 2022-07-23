using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityManagment.PermissionGrants.Dto
{
    [Serializable]
    public class UserInfoForPermissionGrantInputDto
    {
        public Guid UserId { get; set; }

        public IEnumerable<string> Roles { get; set; }

        public string Permission { get; set; }
    }

  
}



