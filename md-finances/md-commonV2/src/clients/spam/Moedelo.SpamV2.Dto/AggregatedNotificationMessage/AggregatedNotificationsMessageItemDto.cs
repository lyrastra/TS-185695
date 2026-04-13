using Moedelo.SpamV2.Dto.PushSender;
using Moedelo.SpamV2.Dto.PushSender.Models;

namespace Moedelo.SpamV2.Dto.AggregatedNotificationMessage
{
    public class AggregatedNotificationsMessageItemDto
    {
        public SmsItemDto SmsData { get; set; }
        public IPushUserData<IPushNotificationData> PushData { get; set; }
    }
}
