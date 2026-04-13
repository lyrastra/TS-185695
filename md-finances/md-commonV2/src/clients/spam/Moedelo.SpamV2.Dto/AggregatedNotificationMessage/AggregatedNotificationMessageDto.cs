using System;
using System.Collections.Generic;

namespace Moedelo.SpamV2.Dto.AggregatedNotificationMessage
{
    public class AggregatedNotificationMessageDto
    {
        public int FirmId { get; set; }
        public int UserId { get; set; }
        public IReadOnlyCollection<AggregatedNotificationsMessageItemDto> Messages { get; set; } = Array.Empty<AggregatedNotificationsMessageItemDto>();
    }
}
