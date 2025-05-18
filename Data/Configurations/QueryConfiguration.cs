using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reunite.Models;

namespace Reunite.Data.Configurations
{
    public class QueryConfiguration : IEntityTypeConfiguration<Query>
    {
        public void Configure(EntityTypeBuilder<Query> builder)
        {
            builder.HasOne(c => c.FacebookPost)
                .WithOne(u => u.Query)
                .HasForeignKey<FacebookPost>(c => c.QueryId)
                .IsRequired();

            builder.HasOne(c => c.Location)
                .WithOne(u => u.Query)
                .HasForeignKey<Location>(c => c.QueryId)
                .IsRequired();
        }
    }
}
