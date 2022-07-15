using IdentityService.ETOs.Users;
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace IdentityService.AuthUsers
{
    public class AuthUser : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// for ef core mapping
        /// </summary>
        protected AuthUser()
        { }

        public AuthUser(Guid id, string cellphone, string email) : base(id)
        {
            Cellphone = cellphone;
            Email = email;

            AddDistributedEvent(new AuthUserCreatedEto(id, Cellphone, Email));
        }

        public string Cellphone { get; protected set; }
        public string Email { get; protected set; }


        public void Remove()
        {
            IsDeleted = true;
            AddDistributedEvent(new AuthUserDeletedEto(Id));
        }
    }
}