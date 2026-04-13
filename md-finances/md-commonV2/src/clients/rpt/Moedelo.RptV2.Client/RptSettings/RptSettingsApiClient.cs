using System.Threading.Tasks;

using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RptV2.Dto.ReportSettings;

namespace Moedelo.RptV2.Client.RptSettings
{
    [InjectAsSingleton]
    public class RptSettingsApiClient : BaseApiClient, IRptSettingsApiClient
    {
        private readonly SettingValue apiEndpoint;
        
        public RptSettingsApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("RptApiEndpoint");
        }
        
        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task Save(int firmId, int userId, RptSettingsSaveRequest request) => PostAsync(
            $"/RptSettings/Save?firmId={firmId}&userId={userId}",
            request
        );

        public Task<RptSettingsDto> GetAsync(int firmId, int userId)
        {
            return GetAsync<RptSettingsDto>("/RptSettings", new { firmId, userId });
        }
    }
}
