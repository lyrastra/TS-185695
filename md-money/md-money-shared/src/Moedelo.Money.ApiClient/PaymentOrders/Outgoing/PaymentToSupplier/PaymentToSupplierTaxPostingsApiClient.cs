using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.Json;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Money.ApiClient.Abstractions.Common.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier.Dto;
using System.Threading.Tasks;

namespace Moedelo.Money.ApiClient.PaymentOrders.Outgoing.PaymentToSupplier
{
    [InjectAsSingleton(typeof(IPaymentToSupplierTaxPostingsApiClient))]
    class PaymentToSupplierTaxPostingsApiClient : BaseApiClient, IPaymentToSupplierTaxPostingsApiClient
    {
        public PaymentToSupplierTaxPostingsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PaymentToSupplierTaxPostingsApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("MoneyApiProvidingEndpoint"),
                logger)
        {
        }

        public async Task<string> GenerateAsync(PaymentToSupplierTaxPostingsGenerateRequestDto generateRequest)
        {
            var url = "/private/api/v1/PaymentOrders/Outgoing/PaymentToSupplier/TaxPostings";
            var result = await PostAsync<PaymentToSupplierTaxPostingsGenerateRequestDto, ApiDataDto<object>>(url, generateRequest);
            return result.data.ToJsonString();
        }
    }
}
