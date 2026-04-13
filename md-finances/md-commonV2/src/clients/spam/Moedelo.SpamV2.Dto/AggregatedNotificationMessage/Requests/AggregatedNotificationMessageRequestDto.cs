using System;
using System.Collections.Generic;
using Moedelo.SpamV2.Dto.PushSender.Models;

namespace Moedelo.SpamV2.Dto.AggregatedNotificationMessage
{
    public class AggregatedNotificationMessageRequestDto<T> where T: IPushNotificationData
    {
        public int FirmId { get; set; }
        public int UserId { get; set; }
        public IReadOnlyCollection<AggregatedNotificationMessageRequestItemDto<T>> Messages { get; set; } = Array.Empty<AggregatedNotificationMessageRequestItemDto<T>>();
    }
}
