using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.CommonApi.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.CommonApi.ApiClient.Abstractions.legacy.Notifications;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.CommonApi.ApiClient.Legacy.Notifications
{
    [InjectAsSingleton(typeof(INotificationsApiClient))]
    internal sealed class NotificationsApiClient : BaseLegacyApiClient, INotificationsApiClient
    {
        public NotificationsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<NotificationsApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("CommonApiPrivateApiEndpoint"),
                logger)
        {
        }

        public Task SaveAsync(FirmId firmId, UserId userId, NotificationSaveRequestDto dto)
        {
            return PostAsync($"/Notifications/Save?firmId={firmId}&userId={userId}", dto);
        }
    }
}