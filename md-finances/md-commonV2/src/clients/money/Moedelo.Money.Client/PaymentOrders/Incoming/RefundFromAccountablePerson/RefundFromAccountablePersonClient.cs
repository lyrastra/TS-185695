using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.Money.Dto;
using Moedelo.Money.Dto.PaymentOrders;
using Moedelo.Money.Dto.PaymentOrders.Incoming.RefundFromAccountablePerson;
using System.Threading.Tasks;

namespace Moedelo.Money.Client.PaymentOrders.Incoming.RefundFromAccountablePerson
{
    [InjectAsSingleton]
    public class RefundFromAccountablePersonClient : BaseCoreApiClient, IRefundFromAccountablePersonClient
    {
        private readonly ISettingRepository settingRepository;
        private const string prefix = "/api/v1";

        public RefundFromAccountablePersonClient(
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

        public async Task<PaymentOrderSaveResponseDto> CreateAsync(int firmId, int userId, RefundFromAccountablePersonSaveDto dto)
        {
            var path = "PaymentOrders/Incoming/RefundFromAccountablePerson";
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await PostAsync<RefundFromAccountablePersonSaveDto, ApiDataResult<PaymentOrderSaveResponseDto>>(
                $"{prefix}/{path}", dto, queryHeaders: tokenHeaders).ConfigureAwait(false);
            return response.data;
        }
    }
}
