using System;
using System.Threading.Tasks;
using Moedelo.Spam.ApiClient.Abastractions.Dto.BuroNotifications;

namespace Moedelo.Spam.ApiClient.Abastractions.Interfaces.BuroNotifications
{
    public interface IBuroTariffStartNotificationsClient
    {
        [Obsolete("Не использовать, отправка переехала в mindbox")]
        Task SendAsync(DateTime? date);

        [Obsolete("Не использовать, отправка переехала в mindbox")]
        Task SendAsync(SendBuroTariffStartNotificationsRequestDto request);
    }
}