using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.InfrastructureV2.Json;
using Moedelo.Spam.ApiClient.Abastractions.Dto.PushNotifications;
using Moedelo.Spam.ApiClient.Abastractions.Dto.PushNotifications.Models;
using Moedelo.Spam.ApiClient.Abastractions.Interfaces.Push;

namespace Moedelo.Spam.ApiClient.Framework.Push
{
    [InjectAsSingleton(typeof(IPushNotificationNetApiClient))]
    internal sealed class PushNotificationNetApiClient(
        IHttpRequestExecutor httpRequestExecutor,
        IUriCreator uriCreator,
        IResponseParser responseParser,
        ISettingRepository settingRepository,
        IAuditTracer auditTracer,
        IAuditScopeManager auditScopeManager)
        : BaseApiClient(
            httpRequestExecutor,
            uriCreator,
            responseParser,
            auditTracer,
            auditScopeManager), IPushNotificationNetApiClient
    {
        private const string ApiRoute = "/api/v1/PushNotification";

        private readonly SettingValue ApiEndPoint = settingRepository.Get("PushNetApiEndpoint");

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
            return PutAsync($"{ApiRoute}/Viewed/{userId}/{pushId}", new { }, cancellationToken: cancellationToken);
        }

        public Task<IReadOnlyList<PushNotificationDto>> GetByIdsAsync(IReadOnlyCollection<int> pushIds, CancellationToken cancellationToken)
        {
            return PostAsync<IReadOnlyCollection<int>, IReadOnlyList<PushNotificationDto>>(
                $"{ApiRoute}/GetByIds",
                pushIds,
                cancellationToken: cancellationToken);
        }

        protected override string GetApiEndpoint()
        {
            return ApiEndPoint.Value;
        }
    }
}