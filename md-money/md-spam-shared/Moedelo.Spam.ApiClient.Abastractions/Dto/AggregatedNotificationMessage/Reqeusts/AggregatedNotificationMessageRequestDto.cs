using System;
using System.Collections.Generic;
using Moedelo.Spam.ApiClient.Abastractions.Dto.PushNotifications.Models;

namespace Moedelo.Spam.ApiClient.Abastractions.Dto.AggregatedNotificationMessage.Reqeusts
{
    public class AggregatedNotificationMessageRequestDto<T> where T: IPushNotificationData
    {
        public int FirmId { get; set; }
        
        public int UserId { get; set; }
        
        public IReadOnlyCollection<AggregatedNotificationMessageItemRequestDto<T>> Messages { get; set; } = Array.Empty<AggregatedNotificationMessageItemRequestDto<T>>();    
    }
}
