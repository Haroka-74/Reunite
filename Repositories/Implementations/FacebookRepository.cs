using Reunite.Data;
using Reunite.Models;
using Reunite.Repositories.Interfaces;

namespace Reunite.Repositories.Implementations;

public class FacebookRepository(ReuniteDbContext context) : IFacebookRepository
{
    public async Task AddFacebookPostAsync(FacebookPost facebookPost)
    {
        context.FacebookPosts.Add(facebookPost);
        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync() => await context.SaveChangesAsync();
}