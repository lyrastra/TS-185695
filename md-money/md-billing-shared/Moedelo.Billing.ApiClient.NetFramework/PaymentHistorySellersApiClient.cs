using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Dto.PaymentHistorySellers;
using Moedelo.Billing.Abstractions.Legacy.Interfaces.PaymentHistorySellers;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Billing.ApiClient.NetFramework
{
    [InjectAsSingleton(typeof(IPaymentHistorySellersApiClient))]
    public class PaymentHistorySellersApiClient : BaseApiClient, IPaymentHistorySellersApiClient
    {
        private const string routePrefix = "/V2/PaymentHistory/Seller";

        private readonly SettingValue apiEndpoint;

        public PaymentHistorySellersApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("InternalBillingApiEndpoint");
        }

        public Task<PaymentHistorySellerDto[]> GetAsync(int paymentHistoryId)
        {
            var uri = $"{routePrefix}?paymentHistoryId={paymentHistoryId}";

            return GetAsync<PaymentHistorySellerDto[]>(uri);
        }

        public Task SaveAsync(PaymentHistorySellersUpdateRequestDto dto)
        {
            var uri = routePrefix;

            return PostAsync(uri, dto);
        }

        public Task<PaymentHistorySellersValidationResponseDto> ValidateAsync(PaymentHistorySellersUpdateRequestDto dto)
        {
            var uri = $"{routePrefix}/Validate";

            return PostAsync<PaymentHistorySellersUpdateRequestDto, PaymentHistorySellersValidationResponseDto>(uri, dto);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }
    }
}