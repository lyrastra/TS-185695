using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Money.ApiClient.Abstractions.Common.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.RefundToCustomer;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.RefundToCustomer.Dto;

namespace Moedelo.Money.ApiClient.PaymentOrders.Outgoing.RefundToCustomer
{
    [InjectAsSingleton(typeof(IRefundToCustomerApiClient))]
    public class RefundToCustomerApiClient: BaseApiClient, IRefundToCustomerApiClient
    {
        public RefundToCustomerApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<RefundToCustomerApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("MoneyApiEndpoint"),
                logger)
        {
        }
        
        public async Task<RefundToCustomerPaymentDto> GetByIdAsync(long documentBaseId)
        {
            var url = $"/api/v1/PaymentOrders/Outgoing/RefundToCustomer/{documentBaseId}";
            var result = await GetAsync<ApiDataDto<RefundToCustomerPaymentDto>>(url);
            return result.data;
        }
    }
}