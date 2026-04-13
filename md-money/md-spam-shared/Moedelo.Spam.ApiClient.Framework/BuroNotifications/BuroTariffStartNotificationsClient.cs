using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.Spam.ApiClient.Abastractions.Dto.BuroNotifications;
using Moedelo.Spam.ApiClient.Abastractions.Interfaces.BuroNotifications;

namespace Moedelo.Spam.ApiClient.Framework.BuroNotifications
{
    [InjectAsSingleton(typeof(IBuroTariffStartNotificationsClient))]
    public class BuroTariffStartNotificationsClient : BaseApiClient, IBuroTariffStartNotificationsClient
    {
        private readonly SettingValue apiEndPoint;
        
        public BuroTariffStartNotificationsClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("SpamApiEndpoint");
        }

        public Task SendAsync(DateTime? date)
        {
            const string uri = "/api/v1/Notification/SendBuroNotification";

            return PostAsync(uri, new { date });
        }

        public Task SendAsync(SendBuroTariffStartNotificationsRequestDto request)
        {
            const string uri = "/api/v1/Notification/SendBuroTariffStartNotifications";

            return PostAsync(uri, request);
        }
        
        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }
    }
}