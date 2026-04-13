using System.Collections.Generic;

namespace Moedelo.CommonApiV2.Dto.Notifications;

public class NotificationDataDto
{
    public NotificationDto Notification { get; set; }

    public List<UserNotificationDto> UserNotifications { get; set; }
}