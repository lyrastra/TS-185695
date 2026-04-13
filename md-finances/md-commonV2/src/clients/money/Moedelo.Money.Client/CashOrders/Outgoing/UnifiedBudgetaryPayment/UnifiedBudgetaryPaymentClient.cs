using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.Money.Dto;
using Moedelo.Money.Dto.CashOrders;
using Moedelo.Money.Dto.CashOrders.Outgoing.UnifiedBudgetaryPayment;
using System.Threading.Tasks;

namespace Moedelo.Money.Client.CashOrders.Outgoing.UnifiedBudgetaryPayment
{
    [InjectAsSingleton]
    public class UnifiedBudgetaryPaymentClient : BaseCoreApiClient, IUnifiedBudgetaryPaymentClient
    {
        private readonly ISettingRepository settingRepository;
        private const string prefix = "/api/v1";

        public UnifiedBudgetaryPaymentClient(
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

        public async Task<UnifiedBudgetaryPaymentGetDto> GetAsync(int firmId, int userId, long documentBaseId)
        {
            var path = "CashOrders/Outgoing/UnifiedBudgetaryPayment";
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await GetAsync<ApiDataResult<UnifiedBudgetaryPaymentGetDto>>(
                $"{prefix}/{path}/{documentBaseId}", queryHeaders: tokenHeaders).ConfigureAwait(false);
            return response.data;
        }

        public async Task<CashOrderSaveResponseDto> CreateAsync(int firmId, int userId, UnifiedBudgetaryPaymentSaveDto dto)
        {
            var path = "CashOrders/Outgoing/UnifiedBudgetaryPayment";
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await PostAsync<UnifiedBudgetaryPaymentSaveDto, ApiDataResult<CashOrderSaveResponseDto>>(
                $"{prefix}/{path}", dto, queryHeaders: tokenHeaders).ConfigureAwait(false);
            return response.data;
        }

        public async Task<CashOrderSaveResponseDto> UpdateAsync(int firmId, int userId, UnifiedBudgetaryPaymentSaveDto dto)
        {
            var path = "CashOrders/Outgoing/UnifiedBudgetaryPayment";
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await PutAsync<UnifiedBudgetaryPaymentSaveDto, ApiDataResult<CashOrderSaveResponseDto>>(
                $"{prefix}/{path}/{dto.DocumentBaseId}", dto, queryHeaders: tokenHeaders).ConfigureAwait(false);
            return response.data;
        }
    }
}
