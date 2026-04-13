using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.InfrastructureV2.Json;
using Moedelo.SpamV2.Dto.PushSender;
using Moedelo.SpamV2.Dto.PushSender.Models;

namespace Moedelo.SpamV2.Client.PushSender
{
#pragma warning disable CS0618 // Type or member is obsolete
    [InjectAsSingleton(typeof(IPushNotificationSenderApiClient))]
    internal sealed class PushNotificationSenderApiClient : BaseApiClient, IPushNotificationSenderApiClient
#pragma warning restore CS0618 // Type or member is obsolete
    {
        private readonly SettingValue apiEndPoint;

        public PushNotificationSenderApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("pushServiceUrl");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        [Obsolete("Необходимо перейти на метод IPushNotificationSenderApiClient.SendToUserAsync")]
        public Task SendAsync(int firmId, PushDataDto pushData)
        {
            return PostAsync($"/Send?firmId={firmId}", pushData);
        }

        public Task SendToUserAsync<T>(int firmId, int userId, PushUserData<T> pushData) where T: IPushNotificationData
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
    }
}