using Microsoft.EntityFrameworkCore;
using Reunite.Models.Auth;
using Reunite.Models.Chats;
using Reunite.Models.Children;

namespace Reunite.Data
{
    public class ReuniteDbContext : DbContext
    {

        public DbSet<ReuniteUser> Users { get; set; }
        public DbSet<Child> Childs { get; set; }
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