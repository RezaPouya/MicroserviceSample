using System;
using Volo.Abp.EventBus;

namespace IdentityManagment.ETOs.Users
{
    [Serializable]
    [EventName("UserService:UserDeleted")]
    public class UserDeletedEto
    {
        public UserDeletedEto()
        {

        }
        public Guid Id { get; set; }

        public UserDeletedEto(Guid id)
        {
            Id = id;
        }
    }
}
