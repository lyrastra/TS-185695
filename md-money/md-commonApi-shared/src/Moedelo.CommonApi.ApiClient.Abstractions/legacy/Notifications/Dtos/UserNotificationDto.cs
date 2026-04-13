using Moedelo.CommonApi.Enums.Notifications;

namespace Moedelo.CommonApi.ApiClient.Abstractions.legacy.Notifications.Dtos
{
    public class UserNotificationDto
    {
        public int Id { get; set; }

        public int FirmId { get; set; }

        public int UserId { get; set; }

        public int NotificationId { get; set; }

        public UserNotificationStatus Status { get; set; }
    }
}
