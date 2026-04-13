using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.Money.Dto;
using Moedelo.Money.Dto.PaymentOrders;
using Moedelo.Money.Dto.PaymentOrders.Outgoing.BudgetaryPayment;
using System.Threading.Tasks;

namespace Moedelo.Money.Client.PaymentOrders.Outgoing.BudgetaryPayment
{
    [InjectAsSingleton]
    public class BudgetaryPaymentClient : BaseCoreApiClient, IBudgetaryPaymentClient
    {
        private readonly ISettingRepository settingRepository;
        private const string prefix = "/api/v1";
        private const string privatePrefix = "/private/api/v1";

        public BudgetaryPaymentClient(
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

        public async Task<PaymentOrderSaveResponseDto> CreateAsync(int firmId, int userId, BudgetaryPaymentSaveDto dto)
        {
            var path = "PaymentOrders/Outgoing/BudgetaryPayment";
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await PostAsync<BudgetaryPaymentSaveDto, ApiDataResult<PaymentOrderSaveResponseDto>>(
                $"{prefix}/{path}", dto, queryHeaders: tokenHeaders).ConfigureAwait(false);
            return response.data;
        }

        public async Task<BudgetaryPaymentGetDto> GetAsync(int firmId, int userId, long documentBaseId)
        {
            var path = "PaymentOrders/Outgoing/BudgetaryPayment";
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await GetAsync<ApiDataResult<BudgetaryPaymentGetDto>>(
                $"{prefix}/{path}/{documentBaseId}", queryHeaders: tokenHeaders).ConfigureAwait(false);
            return response.data;
        }

        public async Task SetPayerKppAsync(int firmId, int userId, long documentBaseId, string kpp)
        {
            var path = $"PaymentOrders/Outgoing/BudgetaryPayment/{documentBaseId}/Payer/Kpp";
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            await PutAsync($"{privatePrefix}/{path}", kpp, queryHeaders: tokenHeaders).ConfigureAwait(false);
        }
    }
}
