using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Money.ApiClient.Abstractions.Common.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto.Providing.AccPostings;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier.Dto;
using System.Threading.Tasks;

namespace Moedelo.Money.ApiClient.PaymentOrders.Outgoing.PaymentToSupplier
{
    [InjectAsSingleton(typeof(IPaymentToSupplierAccPostingsApiClient))]
    class PaymentToSupplierAccPostingsApiClient : BaseApiClient, IPaymentToSupplierAccPostingsApiClient
    {
        public PaymentToSupplierAccPostingsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PaymentToSupplierAccPostingsApiClient> logger)
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

        public async Task<AccPostingsResponseDto> GenerateAsync(PaymentToSupplierAccPostingsGenerateRequestDto generateRequest)
        {
            var url = "/private/api/v1/PaymentOrders/Outgoing/PaymentToSupplier/AccPostings";
            var result = await PostAsync<PaymentToSupplierAccPostingsGenerateRequestDto, ApiDataDto<AccPostingsResponseDto>>(url, generateRequest);
            return result.data;
        }
    }
}
