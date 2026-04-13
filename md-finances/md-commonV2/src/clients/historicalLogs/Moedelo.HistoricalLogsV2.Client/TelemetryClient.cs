using System.Web.Hosting;
using Moedelo.HistoricalLogsV2.Dto;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.HistoricalLogsV2.Client
{
    [InjectAsSingleton]
    public class TelemetryClient : BaseApiClient, ITelemetryClient
    {
        private readonly SettingValue endpointSetting;

        public TelemetryClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            endpointSetting = settingRepository.Get("HistoricalLogsApiEndpoint");
        }
        
        protected override string GetApiEndpoint()
        {
            return endpointSetting.Value;
        }

        public void SendEvent(int firmId, int userId, string eventName, string eventBody)
        {
            HostingEnvironment.QueueBackgroundWorkItem(async ct =>
            {
                var data = new TelemetryEventLogRequestDto
                {
                    FirmId = firmId,
                    UserId = userId,
                    EventName = eventName,
                    EventBody = eventBody
                };
                await PostAsync("/Telemetry/Event", data).ConfigureAwait(false);
            });
        }
    }
}