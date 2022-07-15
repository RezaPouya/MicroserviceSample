using IdentityService.AuthUsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace UserService.Configurations.Users
{
    internal class AuthUserConfiguration : IEntityTypeConfiguration<AuthUser>
    {
        public void Configure(EntityTypeBuilder<AuthUser> builder)
        {
            builder.ToTable("AuthUsers");
            //builder.ToTable("Users", t => t.IsTemporal());

            builder.ConfigureByConvention();
            builder.Ignore(c => c.ExtraProperties);

            builder.Property(c => c.Cellphone).IsRequired().HasMaxLength(16);
            builder.Property(c => c.Email).HasMaxLength(256);
        }
    }
}