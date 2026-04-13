using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.HistoricalLogsV2.Client.EvotorLog
{
    [InjectAsSingleton]
    public class EvotorLogClient : BaseApiClient, IEvotorLogClient
    {
        private readonly SettingValue endpointSetting;

        public EvotorLogClient(IHttpRequestExecutor httpRequestExecutor, IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            endpointSetting = settingRepository.Get("HistoricalLogsApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return endpointSetting.Value;
        }

        public Task CreateSessionAsync(string updateUuid, int firmId, string sessionUuid)
        {
            var dto = new EvotorCreateSessionDto
            {
                UpdateUuid = updateUuid,
                FirmId = firmId,
                SessionUuid = sessionUuid
            };

            return PostAsync("/EvotorLog/CreateSession", dto);
        }

        public Task UpdateZReportDataAsync(string updateUuid, int firmId, string sessionUuid, string zReportData)
        {
            var dto = new EvotorUpdateZReportDataDto
            {
                UpdateUuid = updateUuid,
                FirmId = firmId,
                SessionUuid = sessionUuid,
                ZReportData = zReportData,
            };

            return PostAsync("/EvotorLog/UpdateZReportData", dto);
        }

        public Task UpdateRetailReportDataAsync(string updateUuid, int firmId, string sessionUuid, string retailReportData)
        {
            var dto = new EvotorUpdateRetailReportDataDto
            {
                UpdateUuid = updateUuid,
                FirmId = firmId,
                SessionUuid = sessionUuid,
                RetailReportData = retailReportData,
            };

            return PostAsync("/EvotorLog/UpdateRetailReportData", dto);
        }

        public Task UpdateRetailRefundDataAsync(string updateUuid, int firmId, string sessionUuid, string retailRefundData)
        {
            var dto = new EvotorUpdateRetailRefundDataDto
            {
                UpdateUuid = updateUuid,
                FirmId = firmId,
                SessionUuid = sessionUuid,
                RetailRefundData = retailRefundData,
            };

            return PostAsync("/EvotorLog/UpdateRetailRefundData", dto);
        }

        public Task SaveEvotorIntegrationLogAsync(string evotorUserId, DateTime requestDate, string type, string request, string response, string result, int? firmId = null)
        {
            var dto = new EvotorIntegrationLogDto
            {
                EvotorUserId = evotorUserId,
                RequestDate = requestDate,
                Type = type,
                Request = request,
                Response = response,
                Result = result,
                FirmId = firmId
            };

            return PostAsync("/EvotorLog/SaveEvotorIntegrationLog", dto);
        }
    }

    public class EvotorCreateSessionDto
    {
        public string UpdateUuid { get; set; }
        public int FirmId { get; set; }
        public string SessionUuid { get; set; }
    }

    public class EvotorIntegrationLogDto
    {
        public string EvotorUserId { get; set; }
        public int? FirmId { get; set; }
        public DateTime RequestDate { get; set; }
        public string Type { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public string Result { get; set; }
    }

    public class EvotorUpdateRetailReportDataDto
    {
        public string UpdateUuid { get; set; }
        public int FirmId { get; set; }
        public string SessionUuid { get; set; }
        public string RetailReportData { get; set; }
    }

    public class EvotorUpdateRetailRefundDataDto
    {
        public string UpdateUuid { get; set; }
        public int FirmId { get; set; }
        public string SessionUuid { get; set; }
        public string RetailRefundData { get; set; }
    }

    public class EvotorUpdateZReportDataDto
    {
        public string UpdateUuid { get; set; }
        public int FirmId { get; set; }
        public string SessionUuid { get; set; }
        public string ZReportData { get; set; }
    }
}