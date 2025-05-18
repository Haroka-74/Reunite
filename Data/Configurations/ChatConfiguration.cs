using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reunite.Models;

namespace Reunite.Data.Configurations
{
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.HasOne(c => c.User1).WithMany(u => u.SentChats).HasForeignKey(c => c.UserId1).IsRequired();
            builder.HasOne(c => c.User2).WithMany(u => u.ReceivedChats).HasForeignKey(c => c.UserId2).IsRequired();
            builder.HasMany(c => c.Messages).WithOne(m => m.Chat).HasForeignKey(m => m.ChatId).IsRequired();
        }
    }
}
