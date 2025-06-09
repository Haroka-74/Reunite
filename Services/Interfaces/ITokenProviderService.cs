namespace Reunite.Services.Interfaces
{
    public interface ITokenProviderService
    {
        Task<string> GetTokenAsync();
    }
}