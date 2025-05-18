using Microsoft.EntityFrameworkCore;
using Reunite.Models;

namespace Reunite.Data
{
    public class ReuniteDbContext : DbContext
    {

        public DbSet<ReuniteUser> Users { get; set; }
        public DbSet<Query> Queries { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }

        public ReuniteDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ReuniteDbContext).Assembly);
        }

    }
}