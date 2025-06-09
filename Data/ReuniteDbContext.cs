using Microsoft.EntityFrameworkCore;
using Reunite.Models;

namespace Reunite.Data
{
    public class ReuniteDbContext(DbContextOptions options) : DbContext(options)
    {

        public DbSet<ReuniteUser> Users { get; set; }
        public DbSet<Query> Queries { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<FacebookPost> FacebookPosts { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ReuniteDbContext).Assembly);
        }

    }
}