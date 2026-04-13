using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Billing.Abstractions.Legacy.Dto;
using Moedelo.Billing.Abstractions.Legacy.Interfaces;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Billing.Clients.Legacy;

[InjectAsSingleton(typeof(IPaymentShiftApiClient))]
public class PaymentShiftApiClient(
    IHttpRequestExecuter httpRequestExecutor,
    IUriCreator uriCreator,
    IAuditTracer auditTracer,
    IAuditHeadersGetter auditHeadersGetter,
    ISettingRepository settingRepository,
    ILogger<PaymentShiftApiClient> logger)
    : BaseLegacyApiClient(httpRequestExecutor,
        uriCreator,
        auditTracer,
        auditHeadersGetter,
        settingRepository.Get("InternalBillingApiEndpoint"),
        logger), IPaymentShiftApiClient
{
    public Task ShiftPaymentAsync(PaymentShiftRequestDto dto)
    {
        return PostAsync("/PaymentShift/Shift", dto);
    }
}