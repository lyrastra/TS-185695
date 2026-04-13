using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Billing.Abstractions.Legacy.Dto.PaymentMethods;
using Moedelo.Billing.Abstractions.Legacy.Interfaces;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Billing.Clients.Legacy;

[InjectAsSingleton(typeof(IPaymentMethodsApiClient))]
internal class PaymentMethodsApiClient : BaseLegacyApiClient, IPaymentMethodsApiClient
{
    public PaymentMethodsApiClient(
        IHttpRequestExecuter httpRequestExecutor,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<PaymentMethodsApiClient> logger)
        : base(
            httpRequestExecutor,
            uriCreator,
            auditTracer,
            auditHeadersGetter,
            settingRepository.Get("BillingPaymentMethodsApiEndpoint"),
            logger)
    {
    }

    public Task<IReadOnlyCollection<PaymentMethodDto>> GetByCriteriaAsync(PaymentMethodSearchCriteriaDto dto)
    {
        return PostAsync<PaymentMethodSearchCriteriaDto, IReadOnlyCollection<PaymentMethodDto>>(
            "/Rest/PaymentMethods/GetByCriteria",
            dto);
    }

    public Task<IReadOnlyCollection<PaymentMethodDto>> GetAllAsync()
    {
        return GetAsync<IReadOnlyCollection<PaymentMethodDto>>("/Rest/PaymentMethods/GetAll");
    }
}