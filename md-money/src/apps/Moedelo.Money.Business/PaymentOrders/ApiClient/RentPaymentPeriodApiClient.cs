using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Exceptions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.ApiClient;
using System.Net;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.ApiClient

{
    [InjectAsSingleton(typeof(IRentPaymentPeriodApiClient))]
    internal class RentPaymentPeriodApiClient : BaseApiClient, IRentPaymentPeriodApiClient
    {
        private const string prefix = "/private/api/v1/PaymentOrders/Outgoing/RentPayment/Periods/";

        public RentPaymentPeriodApiClient(
            ISettingRepository settingRepository,
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ILogger<RentPaymentPeriodApiClient> logger)
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

        public async Task<TResponse> PostAsync<TRequest, TResponse>(TRequest ids) where TRequest : class
        {
            try
            {   string path = "GetByPaymentBaseIds";
                var response = await base.PostAsync<TRequest, DataWrapper<TResponse>>($"{prefix}/{path}", ids);
                return response.Data;
            }
            catch (HttpRequestResponseStatusException hrscex)
                when (hrscex.StatusCode == HttpStatusCode.NotFound ||
                hrscex.StatusCode == HttpStatusCode.Conflict)
            {
                throw new OperationNotFoundException();
            }
            catch
            {
                throw;
            }
        }

        private class DataWrapper<T>
        {
            public T Data { get; set; }
        }
    }
}
