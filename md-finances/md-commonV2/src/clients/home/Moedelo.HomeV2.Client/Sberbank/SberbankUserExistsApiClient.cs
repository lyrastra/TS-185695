using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.HomeV2.Client.Sberbank
{
    [InjectAsSingleton]
    public class SberbankUserExistsApiClient : BaseApiClient, ISberbankUserExistsApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public SberbankUserExistsApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("HomePrivateApiEndpoint");
        }

        public Task<SberbankUserExistsResponse> Get(string login) => 
            GetAsync<SberbankUserExistsResponse>("/sberbankUserExists", new { login });

        protected override string GetApiEndpoint()
        {
            return apiEndPoint + "/rest/sberbankUserExists";
        }
    }
}