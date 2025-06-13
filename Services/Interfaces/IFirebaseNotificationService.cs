namespace Reunite.Services.Interfaces
{
    public interface IFirebaseNotificationService
    {
        public Task<string> SendNotification(string token, string title, string body);
        public Task<bool> UpdateUserToken(string userId, string token);
    }
}
