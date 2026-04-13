using System.Threading.Tasks;
using Moedelo.SpamV2.Dto.AggregatedNotificationMessage;
using Moedelo.SpamV2.Dto.PushSender.Models;

namespace Moedelo.SpamV2.Client.AggregateNotification
{
    public interface IAggregatedNotificationMessageClient
    {
        Task SendAsync<T>(AggregatedNotificationMessageDto dto) where T : IPushNotificationData;
    }
}