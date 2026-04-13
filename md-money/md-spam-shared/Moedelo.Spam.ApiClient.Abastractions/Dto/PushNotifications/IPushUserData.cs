using Moedelo.Spam.ApiClient.Abastractions.Dto.PushNotifications.Models;

namespace Moedelo.Spam.ApiClient.Abastractions.Dto.PushNotifications
{
    public interface IPushUserData<out T> where T : IPushNotificationData
    {

    }
}
