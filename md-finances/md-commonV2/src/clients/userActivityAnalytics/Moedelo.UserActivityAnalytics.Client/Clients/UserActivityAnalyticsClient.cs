using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.UserActivityAnalytics.Client.Dto;

namespace Moedelo.UserActivityAnalytics.Client.Clients
{
    [InjectAsSingleton]
    public class UserActivityAnalyticsClient : BaseCoreApiClient, IUserActivityAnalyticsClient
    {
        private readonly SettingValue apiEndPoint;
        private readonly ILogger logger;
        private readonly string Tag = nameof(UserActivityAnalyticsClient);

        public UserActivityAnalyticsClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, 
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, 
            IAuditScopeManager auditScopeManager,
            ILogger logger) 
            : base(httpRequestExecutor, uriCreator, responseParser, settingRepository, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("UserActivityAnalyticsApiEndpoint");
            this.logger = logger;
        }

        public async Task SendEventAsync(int firmId, int userId, EventRequest request)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            logger.Info(Tag, $"Токены для отправки события", extraData: new { tokenHeaders, request, firmId, userId });
            await PostAsync("/api/v1/Event", request, tokenHeaders).ConfigureAwait(false);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }
    }
}