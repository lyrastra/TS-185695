using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Dto;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Json;
using Moedelo.Spam.ApiClient.Abastractions.Dto.PushNotifications;
using Moedelo.Spam.ApiClient.Abastractions.Dto.PushNotifications.Models;
using Moedelo.Spam.ApiClient.Abastractions.Interfaces.Push;

namespace Moedelo.Spam.ApiClient.Push;

[InjectAsSingleton(typeof(IPushNotificationNetApiClient))]
internal sealed class PushNotificationNetApiClient(
    IHttpRequestExecuter httpRequestExecutor,
    IUriCreator uriCreator,
    IAuditTracer auditTracer,
    IAuthHeadersGetter authHeadersGetter,
    IAuditHeadersGetter auditHeadersGetter,
    ISettingRepository settingRepository,
    ILogger<PushNotificationNetApiClient> logger)
        : BaseApiClient(
            httpRequestExecutor,
            uriCreator,
            auditTracer,
            authHeadersGetter,
            auditHeadersGetter,
            settingRepository.Get("PushNetApiEndpoint"),
            logger), IPushNotificationNetApiClient
{
    private const string ApiRoute = "/api/v1/PushNotification";

    public Task<PushNotificationSendResultDto> SendToUserAsync<T>(int userId,
        int firmId,
        PushUserData<T> pushData,
        CancellationToken cancellationToken) where T : IPushNotificationData
    {
        var dto = new PushUserDataDto<T>
        {
            Id = pushData.Id,
            IsDeliveryRequired = pushData.IsDeliveryRequired,
            Type = pushData.Type,
            Data = pushData.Data.ToJsonString(),
            DataTypeName = typeof(T).Name,
            CanBeDeffered = pushData.CanBeDeffered,
            PreferredSendDate = pushData.PreferredSendDate,
        };

        return SendToUserAsync(userId, firmId, dto, cancellationToken);
    }

    public Task<PushNotificationSendResultDto> SendToUserAsync<T>(
        int userId,
        int firmId,
        PushUserDataDto<T> dto,
        CancellationToken cancellationToken) where T : IPushNotificationData
    {
        return PostAsync<PushUserDataDto<T>, PushNotificationSendResultDto>(
            $"{ApiRoute}/SendToUserOfFirm?userId={userId}&firmId={firmId}",
            dto,
            cancellationToken: cancellationToken);
    }

    public Task<bool> CheckEnablePushAsync(
        int userId,
        CancellationToken cancellationToken)
    {
        return GetAsync<bool>($"{ApiRoute}/Enable?userId={userId}", cancellationToken: cancellationToken);
    }

    public Task SetPushAsViewedAsync(
        int userId,
        int pushId,
        CancellationToken cancellationToken)
    {
        return PutAsync($"/Viewed/{userId}/{pushId}", new { }, cancellationToken: cancellationToken);
    }

    public Task<IReadOnlyList<PushNotificationDto>> GetByIdsAsync(IReadOnlyCollection<int> pushIds, CancellationToken cancellationToken)
    {
        return PostAsync<IReadOnlyCollection<int>, IReadOnlyList<PushNotificationDto>>(
            $"{ApiRoute}/GetByIds",
            pushIds,
            cancellationToken: cancellationToken);
    }
}