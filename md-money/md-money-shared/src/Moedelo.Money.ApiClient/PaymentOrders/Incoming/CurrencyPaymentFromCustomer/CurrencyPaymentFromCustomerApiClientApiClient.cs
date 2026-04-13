using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Money.ApiClient.Abstractions.Common.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Incoming.CurrencyPaymentFromCustomer;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Incoming.CurrencyPaymentFromCustomer.Models;

namespace Moedelo.Money.ApiClient.PaymentOrders.Incoming.CurrencyPaymentFromCustomer
{
    [InjectAsSingleton(typeof(ICurrencyPaymentFromCustomerApiClient))]
    public class CurrencyPaymentFromCustomerApiClientApiClient : BaseApiClient, ICurrencyPaymentFromCustomerApiClient
    {
        public CurrencyPaymentFromCustomerApiClientApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<CurrencyPaymentFromCustomerApiClientApiClient> logger)
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

        public async Task<CurrencyPaymentFromCustomerDto> GetByIdAsync(long documentBaseId)
        {
            var url = $"/api/v1/PaymentOrders/Incoming/CurrencyPaymentFromCustomer/{documentBaseId}";
            var result = await GetAsync<ApiDataDto<CurrencyPaymentFromCustomerDto>>(url);
            return result.data;
        }
    }
}