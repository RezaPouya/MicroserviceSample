using System;
using UserService.ETOs;
using Volo.Abp.Domain.Entities.Auditing;

namespace UserService.Users
{
    public class User : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// for ef core mapping
        /// </summary>
        protected User()
        { }

        public User(Guid id, string fname, string lname) : base(id)
        {
            Fname = fname;
            Lname = lname;

            AddDistributedEvent(new UserCreatedEto(id, Fname, Lname));
        }

        public string Fname { get; protected set; }
        public string Lname { get; protected set; }


        public void Remove()
        {
            IsDeleted = true;
            AddDistributedEvent(new UserDeletedEto(Id));
        }
    }
}