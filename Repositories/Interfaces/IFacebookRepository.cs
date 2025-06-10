using Reunite.Models;

namespace Reunite.Repositories.Interfaces;

public interface IFacebookRepository
{
    Task AddFacebookPostAsync(FacebookPost facebookPost);
    Task SaveChangesAsync();
}