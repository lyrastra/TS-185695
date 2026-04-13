using System.Linq;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.InfrastructureV2.Json;
using Moedelo.SpamV2.Dto.AggregatedNotificationMessage;
using Moedelo.SpamV2.Dto.PushSender;
using Moedelo.SpamV2.Dto.PushSender.Models;

namespace Moedelo.SpamV2.Client.AggregateNotification
{
    [InjectAsSingleton(typeof(IAggregatedNotificationMessageClient))]
    internal sealed class AggregatedNotificationMessageClient : BaseCoreApiClient, IAggregatedNotificationMessageClient
    {
        private readonly SettingValue endpoint;

        public AggregatedNotificationMessageClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
                : base(httpRequestExecutor, uriCreator, responseParser, settingRepository, auditTracer, auditScopeManager)
        {
            endpoint = settingRepository.Get("SpamApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return endpoint.Value;
        }

        public async Task SendAsync<T>(AggregatedNotificationMessageDto dto) where T : IPushNotificationData
        {
            var request = new AggregatedNotificationMessageRequestDto<T>
            {
                FirmId = dto.FirmId,
                UserId = dto.UserId,
                Messages = dto.Messages
                    .Select(m => new AggregatedNotificationMessageRequestItemDto<T>
                    {
                        SmsData = m.SmsData,
                        PushData = new PushUserDataDto<T>
                        {
                            Id = (m.PushData as PushUserData<T>).Id,
                            IsDeliveryRequired = (m.PushData as PushUserData<T>).IsDeliveryRequired,
                            Type = (m.PushData as PushUserData<T>).Type,
                            Data = (m.PushData as PushUserData<T>).Data.ToJsonString(),
                            DataTypeName = typeof(T).Name,
                            CanBeDeffered = (m.PushData as PushUserData<T>).CanBeDeffered,
                            PreferredSendDate = (m.PushData as PushUserData<T>).PreferredSendDate,
                        }
                    })
                    .ToList()
            };

            var privateTokens = await GetPrivateTokenHeaders(dto.FirmId, dto.UserId).ConfigureAwait(false);
            await PostAsync("/api/v1/AggregatedNotificationMessage/Send", request, privateTokens).ConfigureAwait(false);
        }
    }
}