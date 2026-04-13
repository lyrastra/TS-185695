using Moedelo.CommonApi.ApiClient.Abstractions.legacy.Dtos;
using System.Threading.Tasks;
using Moedelo.Common.Types;

namespace Moedelo.CommonApi.ApiClient.Abstractions.legacy.Notifications
{
    public interface INotificationsApiClient
    {
        Task SaveAsync(FirmId firmId, UserId userId, NotificationSaveRequestDto dto);
    }
}
