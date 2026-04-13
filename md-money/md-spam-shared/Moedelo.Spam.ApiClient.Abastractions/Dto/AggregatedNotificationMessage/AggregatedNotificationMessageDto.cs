using System;
using System.Collections.Generic;
using Moedelo.Spam.ApiClient.Abastractions.Dto.PushNotifications.Models;

namespace Moedelo.Spam.ApiClient.Abastractions.Dto.AggregatedNotificationMessage
{
    public class AggregatedNotificationsMessageDto
    {
        public int FirmId { get; set; }
        public int UserId { get; set; }
        public IReadOnlyCollection<AggregatedNotificationMessageItemDto> Messages { get; set; } = Array.Empty<AggregatedNotificationMessageItemDto>();
    }
}
