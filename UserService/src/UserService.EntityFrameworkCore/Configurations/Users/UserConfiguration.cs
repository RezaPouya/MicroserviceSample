using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Users;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace UserService.Configurations.Users
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            //builder.ToTable("Users", t => t.IsTemporal());

            builder.ConfigureByConvention();
            builder.Ignore(c => c.ExtraProperties);

            builder.Property(c => c.Fname).IsRequired().HasMaxLength(128);
            builder.Property(c => c.Lname).IsRequired().HasMaxLength(128);
        }
    }
}