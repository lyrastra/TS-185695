using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.Money.Dto;
using System.Threading.Tasks;

namespace Moedelo.Money.Client.CashOrders
{
    [InjectAsSingleton]
    public class CashOrdersApiClient : BaseCoreApiClient, ICashOrdersApiClient
    {
        private readonly ISettingRepository settingRepository;
        private const string prefix = "/private/api/v1/CashOrders";

        public CashOrdersApiClient(
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

        public async Task<long> GetIdByBaseIdAsync(int firmId, int userId, long documentBaseId)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await GetAsync<ApiDataResult<long>>(
                $"{prefix}/{documentBaseId}/Id",
                queryHeaders: tokenHeaders).ConfigureAwait(false);
            return response.data;
        }
    }
}
