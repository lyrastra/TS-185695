using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Exceptions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Money.Providing.Business.PaymentOrders.PaymentToSupplier;

namespace Moedelo.Money.Providing.Business.PaymentOrders.Incoming.PaymentFromCustomer
{
    [InjectAsSingleton(typeof(PaymentFromCustomerApiClient))]
    internal class PaymentFromCustomerApiClient : BaseApiClient
    {
        private const string prefix = "/private/api/v1/Incoming/PaymentFromCustomer";

        public PaymentFromCustomerApiClient(
            ISettingRepository settingRepository,
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ILogger<PaymentToSupplierApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("PaymentOrderApiEndpoint"),
                logger)
        {
        }

        public async Task<bool> IsExistsAsync(long documentBaseId)
        {
            try
            {
                var response = await GetAsync<ApiDataResponseWrapper<object>>($"{prefix}/{documentBaseId}");
                return response.Data != null;
            }
            catch (HttpRequestResponseStatusException hrscex)
                when (hrscex.StatusCode == HttpStatusCode.NotFound)
            {
                return false;
            }
            catch
            {
                throw;
            }
        }

        class ApiDataResponseWrapper<T>
        {
            public T Data { get; set; }
        }
    }
}
