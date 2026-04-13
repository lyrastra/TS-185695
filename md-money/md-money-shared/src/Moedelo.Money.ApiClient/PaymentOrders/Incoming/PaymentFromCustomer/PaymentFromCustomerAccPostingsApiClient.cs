using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Money.ApiClient.Abstractions.Common.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto.Providing.AccPostings;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer.Dto;

namespace Moedelo.Money.ApiClient.PaymentOrders.Incoming.PaymentFromCustomer
{
    [InjectAsSingleton(typeof(IPaymentFromCustomerAccPostingsApiClient))]
    internal class PaymentFromCustomerAccPostingsApiClient : BaseApiClient, IPaymentFromCustomerAccPostingsApiClient
    {
        public PaymentFromCustomerAccPostingsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PaymentFromCustomerAccPostingsApiClient> logger)
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

        public async Task<AccPostingsResponseDto> GenerateAsync(PaymentFromCustomerAccPostingsGenerateRequestDto generateRequest)
        {
            var url = "/private/api/v1/PaymentOrders/Incoming/PaymentFromCustomer/AccPostings";
            var result = await PostAsync<PaymentFromCustomerAccPostingsGenerateRequestDto, ApiDataDto<AccPostingsResponseDto>>(url, generateRequest);
            return result.data;
        }
    }
}
