using FirebaseAdmin.Messaging;
using Reunite.Repositories.Interfaces;
using Reunite.Services.Interfaces;

public class FirebaseNotificationService(IUserRepository userRepository) : IFirebaseNotificationService
{

    public async Task<string> SendNotification(string token, string title, string body)
    {
        var message = new Message()
        {
            Token = token,
            Notification = new Notification
            {
                Title = title,
                Body = body
            },
        };

        return await FirebaseMessaging.DefaultInstance.SendAsync(message);
    }

    public async Task<bool> UpdateUserToken(string userId, string token)
    {
        var user = await userRepository.GetUserAsync(userId);
        if (user == null) return false;
        user.FcmToken = token;
        await userRepository.UpdateUserAsync(userId, user);
        return true;
    }

}
