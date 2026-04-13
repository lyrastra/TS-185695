using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public class TelemetryLogReaderClient : BaseApiClient, ITelemetryLogReaderClient
    {
        private readonly SettingValue endpointSetting;

        public TelemetryLogReaderClient(
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

        public Task<List<TelemetryEventCountDto>> GetSummaryByPeriodAsync(DateTime startTime, DateTime endTime)
        {
            const string uri = "/Telemetry/Summary/ByPeriod";
            return GetAsync<List<TelemetryEventCountDto>>(uri, new {startTime, endTime});
        }

        public Task<List<TelemetryLogEntryDto>> GetEventsByNameAndPeriodAsync(DateTime startTime, DateTime endTime, string eventName)
        {
            const string uri = "/Telemetry/Event/ByNameAndPeriod";
            return GetAsync<List<TelemetryLogEntryDto>>(uri, new { startTime, endTime, eventName });
        }
    }
}