using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Spam.ApiClient.Abastractions.Dto.PushNotifications;
using Moedelo.Spam.ApiClient.Abastractions.Dto.PushNotifications.Models;

namespace Moedelo.Spam.ApiClient.Abastractions.Interfaces.Push;

public interface IPushNotificationNetApiClient
{
    Task<PushNotificationSendResultDto> SendToUserAsync<T>(
        int userId,
        int firmId,
        PushUserData<T> pushData,
        CancellationToken cancellationToken) where T : IPushNotificationData;

    Task<PushNotificationSendResultDto> SendToUserAsync<T>(
        int userId,
        int firmId,
        PushUserDataDto<T> dto,
        CancellationToken cancellationToken) where T : IPushNotificationData;

    Task<bool> CheckEnablePushAsync(
        int userId,
        CancellationToken cancellationToken);

    Task SetPushAsViewedAsync(
        int userId,
        int pushId,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<PushNotificationDto>> GetByIdsAsync(
        IReadOnlyCollection<int> pushIds,
        CancellationToken cancellationToken);
}