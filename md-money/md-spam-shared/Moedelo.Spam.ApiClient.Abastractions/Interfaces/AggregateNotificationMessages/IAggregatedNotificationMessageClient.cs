using System.Threading.Tasks;
using Moedelo.Spam.ApiClient.Abastractions.Dto.AggregatedNotificationMessage;
using Moedelo.Spam.ApiClient.Abastractions.Dto.PushNotifications.Models;

namespace Moedelo.Spam.ApiClient.Abastractions.Interfaces.AggregateNotificationMessages;

public interface IAggregatedNotificationMessageClient
{
    Task SendAsync<T>(AggregatedNotificationsMessageDto dto) where T : IPushNotificationData;
}