using System;
using System.Threading.Tasks;
using Moedelo.SpamV2.Dto.PushSender;
using Moedelo.SpamV2.Dto.PushSender.Models;

namespace Moedelo.SpamV2.Client.PushSender
{
    [Obsolete("Используй IPushNotificationNetApiClient md-spam-shared")]
    public interface IPushNotificationSenderApiClient
    {
        [Obsolete("Используется в старых мобильных приложениях")]
        Task SendAsync(int firmId, PushDataDto pushData);

        Task SendToUserAsync<T>(int firmId, int userId, PushUserData<T> pushData) where T: IPushNotificationData;
    }
}