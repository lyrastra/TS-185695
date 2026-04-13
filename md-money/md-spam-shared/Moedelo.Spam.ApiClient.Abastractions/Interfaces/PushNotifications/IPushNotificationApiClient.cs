using System;
using System.Threading.Tasks;
using Moedelo.Spam.ApiClient.Abastractions.Dto.PushNotifications;
using Moedelo.Spam.ApiClient.Abastractions.Dto.PushNotifications.Models;

namespace Moedelo.Spam.ApiClient.Abastractions.Interfaces.PushNotifications
{
    public interface IPushNotificationApiClient
    {
        [Obsolete("Use IPushNotificationNetApiClient.SendToUserAsync")]
        Task SendToUserAsync<T>(int userId, int firmId, PushUserData<T> pushData) where T: IPushNotificationData;

        [Obsolete("Use IPushNotificationNetApiClient.SendToUserAsync")]
        Task SendToUserAsync<T>(int userId, int firmId, PushUserDataDto<T> dto) where T : IPushNotificationData;

        [Obsolete("Use IPushNotificationNetApiClient.SetPushIsViewedAsync")]
        Task SetPushIsViewedAsync(int userId, int pushId);

        [Obsolete("Use IPushNotificationNetApiClient.CheckEnablePushAsync")]
        Task<bool> CheckEnablePushAsync(int userId);
    }
}