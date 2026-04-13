using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.Money.Dto;
using Moedelo.Money.Dto.BankBalanceHistory;
using System.Threading.Tasks;

namespace Moedelo.Money.Client.BankBalanceHistory
{
    [InjectAsSingleton]
    public class MoneyBankBalanceApiClient : BaseCoreApiClient, IMoneyBankBalanceApiClient
    {
        private readonly ISettingRepository settingRepository;
        private const string prefix = "/private/api/v1";

        public MoneyBankBalanceApiClient(
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
            return settingRepository.Get("MoneyBankBalanceHistoryApiEndpoint").Value;
        }

        public async Task<BankBalanceResponseDto> GetAsync(int firmId, int userId, BankBalanceRequestDto request)
        {
            var uri = $"{prefix}/Balances?startDate={request.StartDate.ToString("yyyy-MM-dd")}&endDate={request.EndDate.ToString("yyyy-MM-dd")}&settlementAccountId={request.SettlementAccountId}";
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await GetAsync<ApiDataResult<BankBalanceResponseDto>>(
                uri, queryHeaders: tokenHeaders).ConfigureAwait(false);
            return response.data;
        }
    }
}
