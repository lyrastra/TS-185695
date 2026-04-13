using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Billing.Abstractions.Legacy.Dto;
using Moedelo.Billing.Abstractions.Legacy.Interfaces;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Billing.Clients.Legacy;

[InjectAsSingleton(typeof(IPaymentHistoryExApiClient))]
public class PaymentHistoryExApiClient : BaseLegacyApiClient, IPaymentHistoryExApiClient
{
    public PaymentHistoryExApiClient(
        IHttpRequestExecuter httpRequestExecutor,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<PaymentHistoryExApiClient> logger)
        : base(
            httpRequestExecutor,
            uriCreator,
            auditTracer,
            auditHeadersGetter,
            settingRepository.Get("InternalBillingApiEndpoint"),
            logger)
    {
    }

    public Task<PaymentHistoryExDto> GetAsync(int paymentId)
    {
        return GetAsync<PaymentHistoryExDto>($"/PaymentHistoryEx/{paymentId}");
    }

    public Task<List<PaymentHistoryExDto>> GetAsync(IReadOnlyCollection<int> paymentIds)
    {
        if (!paymentIds.Any())
        {
            return Task.FromResult(new List<PaymentHistoryExDto>());
        }

        return PostAsync<IEnumerable<int>, List<PaymentHistoryExDto>>("/PaymentHistoryEx/Get", paymentIds);
    }

    public Task<List<PaymentHistoryExDto>> GetAsync(PaymentHistoryExRequestDto criteria)
    {
        return PostAsync<PaymentHistoryExRequestDto, List<PaymentHistoryExDto>>("/PaymentHistoryEx/GetByCriteria",
            criteria);
    }

    public Task<PaymentHistoryExBillDataDto> GetPaymentHistoryExBillDataAsync(int paymentId)
    {
        return GetAsync<PaymentHistoryExBillDataDto>("/V2/GetPaymentHistoryExBillData", new { paymentId });
    }

    public Task<IReadOnlyCollection<PaymentHistoryExBillDataDto>> GetPaymentsHistoryExBillDataAsync(
        IReadOnlyCollection<int> paymentIds)
    {
        return PostAsync<IReadOnlyCollection<int>, IReadOnlyCollection<PaymentHistoryExBillDataDto>>(
            "/V2/GetPaymentsHistoryExBillData", paymentIds);
    }
}