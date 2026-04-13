using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Money.ApiClient.Abstractions.Common.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier.Models;

namespace Moedelo.Money.ApiClient.PaymentOrders.Outgoing.CurrencyPaymentToSupplier
{
    [InjectAsSingleton(typeof(ICurrencyPaymentToSupplierApiClient))]
    public class CurrencyPaymentToSupplierApiClient : BaseApiClient, ICurrencyPaymentToSupplierApiClient
    {
        public CurrencyPaymentToSupplierApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<CurrencyPaymentToSupplierApiClient> logger)
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

        public async Task<CurrencyPaymentToSupplierDto> GetByIdAsync(long documentBaseId)
        {
            var url = $"/api/v1/PaymentOrders/Outgoing/CurrencyPaymentToSupplier/{documentBaseId}";
            var result = await GetAsync<ApiDataDto<CurrencyPaymentToSupplierDto>>(url);
            return result.data;
        }
    }
}