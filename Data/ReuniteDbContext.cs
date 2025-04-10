using Microsoft.EntityFrameworkCore;
using Reunite.Models.Auth;
using Reunite.Models.Children;

namespace Reunite.Data
{
    public class ReuniteDbContext : DbContext
    {

        public DbSet<ReuniteUser> Users { get; set; }
        public DbSet<FoundChild> FoundChilds { get; set; }
        public DbSet<MissedChild> MissedChilds { get; set; }

        public ReuniteDbContext(DbContextOptions options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ReuniteDbContext).Assembly);
        }

    }
}