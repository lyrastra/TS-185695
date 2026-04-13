using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Dto.Gateway.Payments;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Billing.Abstractions.Gateway;

public interface IBillingGatewayPaymentsApiClient
{
    Task<BillingGatewayPaymentSummaryDto[]> GetActualPaymentsAsync(
        BillingGatewayPaymentsCriteriaDto criteria,
        HttpQuerySetting setting = null,
        CancellationToken cancellationToken = default);

    Task<int[]> GetPaidFirmIdsByCriteriaAsync(BillingGatewayPaymentPaidFirmIdsDto criteria,
        HttpQuerySetting setting = null,
        CancellationToken cancellationToken = default);

    Task<List<BillingGatewayPaymentSummaryDto>> GetAllPeriodSuccessPaymentsAsync(
        int firmId,
        BillingGatewaySuccessPaymentsCriteriaDto criteriaDto,
        HttpQuerySetting setting = null,
        CancellationToken cancellationToken = default);
}
