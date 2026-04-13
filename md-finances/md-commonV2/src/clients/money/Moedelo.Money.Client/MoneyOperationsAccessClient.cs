using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.Money.Dto;

namespace Moedelo.Money.Client
{
    [InjectAsSingleton]
    public class MoneyOperationsAccessClient : BaseCoreApiClient, IMoneyOperationsAccessClient
    {
        private readonly ISettingRepository settingRepository;

        public MoneyOperationsAccessClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser,
                settingRepository,
                auditTracer,
                auditScopeManager)
        {
            this.settingRepository = settingRepository;
        }

        protected override string GetApiEndpoint()
        {
            return settingRepository.Get("MoneyApiEndpoint").Value;
        }
        
        public async Task<OperationsAccessDto> GetAsync(int firmId, int userId)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            
            var response = await GetAsync<ApiDataResult<OperationsAccessDto>>(
                uri:"/api/v1/operations/access", 
                queryHeaders: tokenHeaders).ConfigureAwait(false);
            
            return response.data;
        }
    }
}