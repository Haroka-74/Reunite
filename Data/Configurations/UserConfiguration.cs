using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reunite.Models.Auth;

namespace Reunite.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ReuniteUser>
    {

        public void Configure(EntityTypeBuilder<ReuniteUser> builder)
        {
            builder.HasMany(c => c.FoundChilds).WithOne(p => p.User).HasForeignKey(p => p.UserId).IsRequired();
            builder.HasMany(c => c.MissedChilds).WithOne(p => p.User).HasForeignKey(p => p.UserId).IsRequired();
        }

    }
}