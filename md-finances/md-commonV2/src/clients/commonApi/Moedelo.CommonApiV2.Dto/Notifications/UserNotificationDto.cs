using Moedelo.Common.Enums.Enums.Notifications;

namespace Moedelo.CommonApiV2.Dto.Notifications;

public class UserNotificationDto
{
    public int Id { get; set; }

    public int FirmId { get; set; }

    public int UserId { get; set; }

    public int NotificationId { get; set; }

    public UserNotificationStatus Status { get; set; }
}