using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Billing.Abstractions.Legacy.Dto;
using Moedelo.Billing.Abstractions.Legacy.Interfaces;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Billing.Clients.Legacy;

[InjectAsSingleton(typeof(IBillingApiClient))]
internal sealed class BillingApiClient : BaseLegacyApiClient, IBillingApiClient
{
    public BillingApiClient(
        IHttpRequestExecuter httpRequestExecutor,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<BillingApiClient> logger)
        : base(httpRequestExecutor,
            uriCreator,
            auditTracer,
            auditHeadersGetter,
            settingRepository.Get("InternalBillingApiEndpoint"),
            logger)
    {
    }

    public Task<PaymentHistoryDto> GetLastTariffWithTrialAsync(FirmId firmId)
    {
        return GetAsync<PaymentHistoryDto>($"/GetLastTariffWithTrial?firmId={firmId}");
    }

    public Task<TariffDto> GetTariffByPriceListIdAsync(int id)
    {
        return GetAsync<TariffDto>($"/GetTariffByPriceListId?id={id}");
    }

    public Task SwitchOnPaymentAsync(SwitchOnPaymentRequestDto switchOnPaymentRequestDto)
    {
        return PostAsync("/SwitchOnPayment", switchOnPaymentRequestDto);
    }

    public Task SwitchOffPaymentAsync(SwitchOffPaymentRequestDto requestDto)
    {
        return PostAsync("/SwitchOffPayment", requestDto);
    }

    public Task<PriceListDto> GetPriceListByIdAsync(int id)
    {
        return GetAsync<PriceListDto>($"/GetPriceListById?id={id}");
    }

    public Task<int> SavePaymentHistoryAsync(PaymentHistoryDto paymentHistory)
    {
        return PostAsync<PaymentHistoryDto, int>("/V2/SavePaymentHistory", paymentHistory);
    }

    public Task<IReadOnlyCollection<PositionByPaymentDto>> GetActsByPaymentAsync(int paymentId)
    {
        return GetAsync<IReadOnlyCollection<PositionByPaymentDto>>("/V2/GetActsByPayment", new { paymentId });
    }

    public Task<PaymentHistoryDto> GetFirstUnsuccessfulPaymentForGroupAsync(int anyPaymentIdInGroup, long transactionId)
    {
        return GetAsync<PaymentHistoryDto>("/GetFirstUnsuccessfulPaymentForGroupAsync", new { anyPaymentIdInGroup, transactionId });
    }
}