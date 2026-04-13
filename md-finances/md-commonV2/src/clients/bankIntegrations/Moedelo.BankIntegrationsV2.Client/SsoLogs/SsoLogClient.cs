using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BankIntegrationsV2.Dto.SsoLogs;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BankIntegrationsV2.Client.SsoLogs
{
    [InjectAsSingleton]
    public class SsoLogClient : BaseApiClient, ISsoLogClient
    {
        private const string ControllerName = "/SsoLog/";
        private readonly SettingValue apiEndPoint;

        public SsoLogClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("IntegrationApi");
        }

        public Task<SsoLogFileDto> GetLogAsync(int id)
        {
            return GetAsync<SsoLogFileDto>("GetLog", new { id }, setting: new HttpQuerySetting(new TimeSpan(0, 0, 0, 120)));
        }

        public Task<List<SsoLogRequestDto>> GetLogRequestsAsync(string clientId, DateTime startDate, DateTime endDate)
        {
            return GetAsync<List<SsoLogRequestDto>>("GetLogRequests", new { clientId, startDate, endDate });
        }

        public Task SaveAsync(SsoLogSaveRequestDto dto)
        {
            return PostAsync("Save", dto);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + ControllerName;
        }
    }
}
