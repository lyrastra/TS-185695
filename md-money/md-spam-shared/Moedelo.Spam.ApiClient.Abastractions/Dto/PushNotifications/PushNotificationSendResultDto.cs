using Moedelo.Spam.ApiClient.Abastractions.Enums.PushNotifications;

namespace Moedelo.Spam.ApiClient.Abastractions.Dto.PushNotifications;

public class PushNotificationSendResultDto
{
    /// <summary>
    /// Идентификатор push-сообщения
    /// </summary>
    public int? PushId { get; set; }

    /// <summary>
    /// Статус отправки push-сообщения
    /// </summary>
    public PushNotificationSendStatus Status { get; set; }
}