using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders;
using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.PaymentOrders;

[InjectAsSingleton(typeof(IPaymentOrderDuplicatesApiClient))]
internal sealed class PaymentOrderDuplicatesApiClient : BaseApiClient, IPaymentOrderDuplicatesApiClient
{
    public PaymentOrderDuplicatesApiClient(
        IHttpRequestExecuter httpRequestExecuter,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuthHeadersGetter authHeadersGetter,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<PaymentOrderApiClient> logger)
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

    public Task MergeAsync(long documentBaseId, CancellationToken ctx)
    {
        var url = $"/api/v1/PaymentOrders/Duplicates/{documentBaseId}/Merge";
        return PostAsync(url, cancellationToken: ctx);
    }
}