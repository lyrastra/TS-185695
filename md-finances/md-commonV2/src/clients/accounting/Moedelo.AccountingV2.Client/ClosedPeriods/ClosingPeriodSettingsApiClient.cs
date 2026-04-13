using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.ClosedPeriods;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.ClosedPeriods
{
    [InjectAsSingleton]
    public class ClosingPeriodSettingsApiClient : BaseApiClient, IClosingPeriodSettingsApiClient
    {
        private readonly SettingValue apiEndPoint;

        public ClosingPeriodSettingsApiClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, 
            IAuditTracer auditTracer, 
            IAuditScopeManager auditScopeManager,  
            ISettingRepository settingRepository)
            : base(
                httpRequestExecutor, 
                uriCreator, 
                responseParser,
                auditTracer,
                auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountingApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<ClosingPeriodSettingsDto> GetAsync(int firmId, int userId)
        {
            return GetAsync<ClosingPeriodSettingsDto>("/ClosingPeriodSettings", new { firmId, userId });
        }
    }
}