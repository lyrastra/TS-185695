using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System.Threading.Tasks;

namespace Moedelo.OutSystemsIntegrationV2.Client.MneZvonili
{
    [InjectAsSingleton]
    public class MneZvoniliClient : BaseApiClient, IMneZvoniliClient
    {
        private readonly SettingValue apiEndpoint;
        
        public MneZvoniliClient(IHttpRequestExecutor httpRequestExecutor, IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("MneZvoniliServiceApiEndpoint");
        }
        
        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/V2";
        }

        public Task<int> GetRegionIdByPhoneAsync(string phone)
        {
            return GetAsync<int>("/GetRegionIdByPhone", new { phone });
        }
    }
}