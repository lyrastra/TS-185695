using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Payroll.ApiClient.Abstractions.PaymentEvents;

namespace Moedelo.Payroll.ApiClient.PaymentEvents
{
    [InjectAsSingleton(typeof(IPaymentEventApiClient))]
    public class PaymentEventApiClient : BaseLegacyApiClient, IPaymentEventApiClient
    {
        public PaymentEventApiClient(
            IHttpRequestExecuter httpRequestExecutor,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PaymentEventApiClient> logger)
            : base(httpRequestExecutor,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("PayrollPrivateApi"),
                logger)
        {
        }

        public Task<byte[]> GetPaymentEventFileAsync(int firmId, int paymentEventFileId)
        {
            return GetAsync<byte[]>("/PaymentEvents/GetPaymentEventFile", new { firmId, paymentEventFileId });
        }
    }
}