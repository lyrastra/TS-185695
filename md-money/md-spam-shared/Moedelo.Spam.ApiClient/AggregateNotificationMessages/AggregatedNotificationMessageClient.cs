using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Json;
using Moedelo.Spam.ApiClient.Abastractions.Dto.AggregatedNotificationMessage;
using Moedelo.Spam.ApiClient.Abastractions.Dto.AggregatedNotificationMessage.Reqeusts;
using Moedelo.Spam.ApiClient.Abastractions.Dto.PushNotifications;
using Moedelo.Spam.ApiClient.Abastractions.Dto.PushNotifications.Models;
using Moedelo.Spam.ApiClient.Abastractions.Interfaces.AggregateNotificationMessages;

namespace Moedelo.Spam.ApiClient.AggregateNotificationMessages;

[InjectAsSingleton(typeof(IAggregatedNotificationMessageClient))]
public class AggregatedNotificationMessageClient : BaseApiClient, IAggregatedNotificationMessageClient
{
    public AggregatedNotificationMessageClient(
        IHttpRequestExecuter httpRequestExecuter,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuthHeadersGetter authHeadersGetter,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<AggregatedNotificationMessageClient> logger)
        : base (httpRequestExecuter,
            uriCreator,
            auditTracer,
            authHeadersGetter,
            auditHeadersGetter,
            settingRepository.Get("SpamApiEndpoint"),
            logger)
    {
    }

    public async Task SendAsync<T>(AggregatedNotificationsMessageDto dto) where T: IPushNotificationData
    {
        var request = new AggregatedNotificationMessageRequestDto<T>
        {
            FirmId = dto.FirmId,
            UserId = dto.UserId,
            Messages = dto.Messages
                .Select(m => new AggregatedNotificationMessageItemRequestDto<T>
                {
                    SmsData = m.SmsData,
                    PushData = new PushUserDataDto<T>
                    {
                        CanBeDeffered = (m.PushData as PushUserData<T>).CanBeDeffered,
                        Id = (m.PushData as PushUserData<T>).Id,
                        IsDeliveryRequired = (m.PushData as PushUserData<T>).IsDeliveryRequired,
                        Type = (m.PushData as PushUserData<T>).Type,
                        Data = (m.PushData as PushUserData<T>).Data.ToJsonString(),
                        DataTypeName = typeof(T).Name
                    }
                })
                .ToList()
        };

        await PostAsync("/api/v1/AggregatedNotificationMessage/Send", request);
    }   
}