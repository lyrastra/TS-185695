using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Spam.ApiClient.Abastractions.Dto.BuroNotifications;
using Moedelo.Spam.ApiClient.Abastractions.Interfaces.BuroNotifications;

namespace Moedelo.Spam.ApiClient.BuroNotifications
{
    [InjectAsSingleton(typeof(IBuroTariffStartNotificationsClient))]
    internal sealed class BuroTariffStartNotificationsClient(
        IHttpRequestExecuter httpRequestExecuter,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuthHeadersGetter authHeadersGetter,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<BuroTariffStartNotificationsClient> logger) : BaseApiClient(httpRequestExecuter,
            uriCreator,
            auditTracer,
            authHeadersGetter,
            auditHeadersGetter,
            settingRepository.Get("SpamApiEndpoint"),
            logger), IBuroTariffStartNotificationsClient
    {
        [Obsolete("Не использовать, отправка переехала в mindbox")]
        public Task SendAsync(DateTime? date)
        {
            const string uri = "/api/v1/Notification/SendBuroNotification";

            return PostAsync(uri, new { date });
        }

        [Obsolete("Не использовать, отправка переехала в mindbox")]
        public Task SendAsync(SendBuroTariffStartNotificationsRequestDto request)
        {
            const string uri = "/api/v1/Notification/SendBuroTariffStartNotifications";

            return PostAsync<SendBuroTariffStartNotificationsRequestDto>(uri, request);
        }
    }
}