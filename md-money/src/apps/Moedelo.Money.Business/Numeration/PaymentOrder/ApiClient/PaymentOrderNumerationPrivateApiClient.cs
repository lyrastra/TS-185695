using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Money.Domain.PaymentOrderNumeration;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Numeration.PaymentOrder.ApiClient
{
    public class PaymentOrderNumerationPrivateApiClient : BaseApiClient, IPaymentOrderNumerationPrivateApiClient
    {
        private const string prefix = "/private/api/v1/PaymentOrderNumeration";

        public PaymentOrderNumerationPrivateApiClient(
            ISettingRepository settingRepository,
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ILogger<PaymentOrderNumerationPrivateApiClient> logger)
            : base(
                  httpRequestExecuter,
                  uriCreator,
                  auditTracer,
                  authHeadersGetter,
                  auditHeadersGetter,
                  settingRepository.Get("MoneyNumerationApiEndpoint"),
                  logger)
        {
        }

        public Task SetLastNumberAsync(PaymentOrderNumerationData model)
        {
            return PostAsync($"{prefix}", PaymentOrderNumerationMapper.MapToDto(model));
        }
    }
}
