using Moedelo.Spam.ApiClient.Abastractions.Dto.PushNotifications;
using Moedelo.Spam.ApiClient.Abastractions.Dto.PushNotifications.Models;

namespace Moedelo.Spam.ApiClient.Abastractions.Dto.AggregatedNotificationMessage
{
    public class AggregatedNotificationMessageItemDto
    {
        public SmsItemDto SmsData { get; set; }
        
        public IPushUserData<IPushNotificationData> PushData { get; set; }
    }
}
