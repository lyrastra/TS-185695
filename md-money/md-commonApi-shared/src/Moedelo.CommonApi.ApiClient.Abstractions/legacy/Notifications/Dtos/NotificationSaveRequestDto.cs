using Moedelo.CommonApi.ApiClient.Abstractions.legacy.Notifications.Dtos;
using System.Collections.Generic;

namespace Moedelo.CommonApi.ApiClient.Abstractions.legacy.Dtos
{
    public class NotificationSaveRequestDto
    {
        public NotificationDto Notification { get; set; }

        public IReadOnlyCollection<UserNotificationDto> UserNotifications { get; set; }
    }
}
