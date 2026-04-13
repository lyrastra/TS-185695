using Moedelo.Spam.ApiClient.Abastractions.Dto.PushNotifications;
using Moedelo.Spam.ApiClient.Abastractions.Dto.PushNotifications.Models;

namespace Moedelo.Spam.ApiClient.Abastractions.Dto.AggregatedNotificationMessage.Reqeusts
{
    public class AggregatedNotificationMessageItemRequestDto<T> where T : IPushNotificationData
    {
        public SmsItemDto SmsData { get; set; }
        
        public PushUserDataDto<T> PushData { get; set; }
    }
}
