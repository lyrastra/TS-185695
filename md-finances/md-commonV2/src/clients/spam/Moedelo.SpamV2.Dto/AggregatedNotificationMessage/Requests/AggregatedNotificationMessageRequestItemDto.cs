using Moedelo.SpamV2.Dto.PushSender;
using Moedelo.SpamV2.Dto.PushSender.Models;

namespace Moedelo.SpamV2.Dto.AggregatedNotificationMessage
{
    public class AggregatedNotificationMessageRequestItemDto<T> where T : IPushNotificationData
    {
        public SmsItemDto SmsData { get; set; }
        public PushUserDataDto<T> PushData { get; set; }
    }
}
