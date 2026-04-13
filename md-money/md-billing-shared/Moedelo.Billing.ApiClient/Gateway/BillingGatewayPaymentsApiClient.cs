using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Billing.Abstractions.Dto.Gateway.Payments;
using Moedelo.Billing.Abstractions.Gateway;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Billing.Clients.Gateway;

[InjectAsSingleton(typeof(IBillingGatewayPaymentsApiClient))]
public class BillingGatewayPaymentsApiClient(
    IHttpRequestExecuter httpRequestExecutor,
    IUriCreator uriCreator,
    IAuditTracer auditTracer,
    IAuthHeadersGetter authHeadersGetter,
    IAuditHeadersGetter auditHeadersGetter,
    ISettingRepository settingRepository,
    ILogger<BillingGatewayPaymentsApiClient> logger)
    :BaseApiClient(
        httpRequestExecutor,
        uriCreator,
        auditTracer,
        authHeadersGetter,
        auditHeadersGetter,
        settingRepository.Get("GatewayBillingApiEndpoint"),
        logger), IBillingGatewayPaymentsApiClient
{
    public Task<BillingGatewayPaymentSummaryDto[]> GetActualPaymentsAsync(
        BillingGatewayPaymentsCriteriaDto criteria,
        HttpQuerySetting setting = null,
        CancellationToken cancellationToken = default)
    {
        const string uri = "/v1/billingState/getActualPayments";

        return PostAsync<BillingGatewayPaymentsCriteriaDto, BillingGatewayPaymentSummaryDto[]>(
            uri, criteria, setting: setting, cancellationToken: cancellationToken);
    }

    public Task<int[]> GetPaidFirmIdsByCriteriaAsync(BillingGatewayPaymentPaidFirmIdsDto criteria,
        HttpQuerySetting setting = null,
        CancellationToken cancellationToken = default)
    {
        const string uri = "/v1/billingState/getPaidFirmIdsByCriteria";

        return PostAsync<BillingGatewayPaymentPaidFirmIdsDto, int[]>(
            uri, criteria, setting: setting, cancellationToken: cancellationToken);
    }

    public Task<List<BillingGatewayPaymentSummaryDto>> GetAllPeriodSuccessPaymentsAsync(
        int firmId,
        BillingGatewaySuccessPaymentsCriteriaDto criteriaDto,
        HttpQuerySetting setting = null,
        CancellationToken cancellationToken = default)
    {
        const string successUri = "/v1/billingState/successPayments";

        var uri = $"{successUri}/{firmId}/getAllPeriod";

        return PostAsync<
            BillingGatewaySuccessPaymentsCriteriaDto,
            List<BillingGatewayPaymentSummaryDto>>(
                uri, criteriaDto, setting: setting, cancellationToken: cancellationToken);
    }
}
