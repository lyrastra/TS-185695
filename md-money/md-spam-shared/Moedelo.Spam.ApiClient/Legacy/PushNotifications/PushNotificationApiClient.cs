using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Json;
using Moedelo.Spam.ApiClient.Abastractions.Dto;
using Moedelo.Spam.ApiClient.Abastractions.Dto.PushNotifications;
using Moedelo.Spam.ApiClient.Abastractions.Dto.PushNotifications.Models;
using Moedelo.Spam.ApiClient.Abastractions.Interfaces.PushNotifications;

namespace Moedelo.Spam.ApiClient.Legacy.PushNotifications;

[InjectAsSingleton(typeof(IPushNotificationApiClient))]
internal sealed class PushNotificationApiClient(
    IHttpRequestExecuter httpRequestExecuter,
    IUriCreator uriCreator,
    IAuditTracer auditTracer,
    IAuditHeadersGetter auditHeadersGetter,
    ISettingRepository settingRepository,
    ILogger<PushNotificationApiClient> logger) : BaseLegacyApiClient(
        httpRequestExecuter,
        uriCreator,
        auditTracer,
        auditHeadersGetter,
        settingRepository.Get("pushServiceUrl"),
        logger), IPushNotificationApiClient
{
    [Obsolete("Use IPushNotificationNetApiClient.SendToUserAsync")]
    public Task SendToUserAsync<T>(int userId, int firmId, PushUserData<T> pushData) where T : IPushNotificationData
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

        return PostAsync($"/SendToUser?userId={userId}&firmId={firmId}", dto);
    }

    [Obsolete("Use IPushNotificationNetApiClient.SendToUserAsync")]
    public Task SendToUserAsync<T>(int userId, int firmId, PushUserDataDto<T> dto) where T : IPushNotificationData
    {
        return PostAsync($"/SendToUser?userId={userId}&firmId={firmId}", dto);
    }

    [Obsolete("Use IPushNotificationNetApiClient.SetPushIsViewedAsync")]
    public Task SetPushIsViewedAsync(int userId, int pushId)
    {
        return PutAsync($"/Viewed?userId={userId}&pushId={pushId}", new { });
    }

    [Obsolete("Use IPushNotificationNetApiClient.CheckEnablePushAsync")]
    public async Task<bool> CheckEnablePushAsync(int userId)
    {
        var response = await GetAsync<DataWrapper<bool>>($"/Enable?userId={userId}");
        return response.Data;
    }
}