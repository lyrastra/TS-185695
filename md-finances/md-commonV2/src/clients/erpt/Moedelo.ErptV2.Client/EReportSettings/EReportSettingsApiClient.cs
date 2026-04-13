using System.Collections.Generic;
using System.Threading;
using Moedelo.Common.Enums.Enums.ElectronicReports;
using Moedelo.ErptV2.Dto.EreportSettings;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.ErptV2.Client.EReportSettings
{
    [InjectAsSingleton(typeof(IEReportSettingsApiClient))]
    public class EReportSettingsApiClient : BaseApiClient, IEReportSettingsApiClient
    {
        private readonly SettingValue apiEndpoint;
        
        public EReportSettingsApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("ErptApiEndpoint");
        }
        
        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<bool> CanSend(int firmId, int userId, FundType fund, string code, string route, CancellationToken cancellationToken)
        {
            return GetAsync<bool>("/erptsettings/CanSendV2", new { firmId, userId, fund, code, route }, cancellationToken: cancellationToken);
        }

        public Task<ProviderRegionInfoDto> GetProviderRegionInfo(int firmId)
        {
            return GetAsync<ProviderRegionInfoDto>("/erptsettings/GetProviderRegionInfo", new { firmId });
        }

        public Task<SendingSettings> GetSendgingSettings(int firmId, int userId)
        {
            return GetAsync<SendingSettings>("/erptsettings/GetSendingSettingsV2", new { firmId, userId });
        }
        
        public Task<List<SendingDirection>> GetSendingDirections(int firmId, int userId)
        {
            return GetAsync<List<SendingDirection>>("/erptsettings/GetSendingDirections", new { firmId, userId });
        }
    }
}