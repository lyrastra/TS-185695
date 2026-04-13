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
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Billing.Clients.Legacy;

[InjectAsSingleton(typeof(IPaymentHistoryApiClient))]
internal sealed class PaymentHistoryApiClient : BaseLegacyApiClient, IPaymentHistoryApiClient
{
    public PaymentHistoryApiClient(
        IHttpRequestExecuter httpRequestExecutor,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<PaymentHistoryApiClient> logger)
        : base(
            httpRequestExecutor,
            uriCreator,
            auditTracer,
            auditHeadersGetter,
            settingRepository.Get("InternalBillingApiEndpoint"),
            logger)
    {
    }

    public Task<List<PaymentHistoryDto>> GetPaymentHistoryForFirmAsync(int firmId)
    {
        return GetAsync<List<PaymentHistoryDto>>("/V2/GetPaymentHistoryForFirm", new { firmId });
    }

    public Task<int> SavePaymentHistoryAsync(PaymentHistoryDto paymentHistory)
    {
        return PostAsync<PaymentHistoryDto, int>("/V2/SavePaymentHistory", paymentHistory);
    }

    public Task<List<PaymentHistoryDto>> GetByCriteriaAsync(PaymentHistoryRequestDto paymentHistoryRequestDto)
    {
        return PostAsync<PaymentHistoryRequestDto, List<PaymentHistoryDto>>("/PaymentHistory/GetBy",
            paymentHistoryRequestDto);
    }

    public Task<FirmPackagesInfoDto[]> GetFirmsPaidPackages(IReadOnlyCollection<int> firmIds)
    {
        return PostAsync<IReadOnlyCollection<int>, FirmPackagesInfoDto[]>(
            "/PaymentHistory/GetFirmsPaidPackages",
            firmIds);
    }

    public Task<List<PaymentPositionDto>> GetPositionsAsync(int paymentHistoryId)
    {
        return GetAsync<List<PaymentPositionDto>>($"/PaymentHistory/{paymentHistoryId}/positions");
    }

    public Task<Dictionary<int, List<PaymentPositionDto>>> GetPositionsAsync(IReadOnlyCollection<int> paymentHistoryIds)
    {
        return PostAsync<IReadOnlyCollection<int>, Dictionary<int, List<PaymentPositionDto>>>(
            "/PaymentHistory/GetPositions",
            paymentHistoryIds);
    }

    public Task UpdateIncomingDateAsync(UpdateIncomingDateRequestDto requestDto)
    {
        return PostAsync("/PaymentHistory/UpdateIncomingDate", requestDto);
    }

    public Task<PaymentHistoryDto> GetAsync(int paymentHistoryId)
    {
        return GetAsync<PaymentHistoryDto>($"/PaymentHistory/{paymentHistoryId}");
    }

    public Task MarkAsExportedTo1CAsync(PaymentHistoryMarkAsExportedTo1CRequestDto requestDto,
        HttpQuerySetting setting = null)
    {
        if (requestDto?.PaymentIds?.Any() != true)
        {
            return Task.CompletedTask;
        }

        return PostAsync("/PaymentHistory/MarkAsExportedTo1c", requestDto, setting: setting);
    }
}