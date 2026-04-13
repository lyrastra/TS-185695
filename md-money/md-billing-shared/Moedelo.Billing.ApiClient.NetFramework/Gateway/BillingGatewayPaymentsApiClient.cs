using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Dto.Gateway.Payments;
using Moedelo.Billing.Abstractions.Gateway;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using InfrastrcutureV2HttpQuerySetting
    = Moedelo.InfrastructureV2.Domain.Models.ApiClient.HttpQuerySetting;

namespace Moedelo.Billing.ApiClient.NetFramework.Gateway;

[InjectAsSingleton(typeof(IBillingGatewayPaymentsApiClient))]
public class BillingGatewayPaymentsApiClient(
    IHttpRequestExecutor httpRequestExecutor,
    IUriCreator uriCreator,
    IResponseParser responseParser,
    IAuditTracer auditTracer,
    IAuditScopeManager auditScopeManager,
    ISettingRepository settingRepository)
    : BaseApiClient(
        httpRequestExecutor,
        uriCreator,
        responseParser,
        auditTracer,
        auditScopeManager), IBillingGatewayPaymentsApiClient
{
    private readonly SettingValue apiEndpoint
        = settingRepository.Get("GatewayBillingApiEndpoint").ThrowExceptionIfNull(true);

    public Task<BillingGatewayPaymentSummaryDto[]> GetActualPaymentsAsync(
        BillingGatewayPaymentsCriteriaDto criteria,
        HttpQuerySetting setting = null,
        CancellationToken cancellationToken = default)
    {
        const string uri = "/v1/billingState/getActualPayments";

        return PostAsync<BillingGatewayPaymentsCriteriaDto, BillingGatewayPaymentSummaryDto[]>(
            uri, criteria, setting: BuildHttpQuerySetting(setting), cancellationToken: cancellationToken);
    }

    public Task<int[]> GetPaidFirmIdsByCriteriaAsync(
        BillingGatewayPaymentPaidFirmIdsDto criteria,
        HttpQuerySetting setting = null,
        CancellationToken cancellationToken = default)
    {
        const string uri = "/v1/billingState/getPaidFirmIdsByCriteria";

        return PostAsync<BillingGatewayPaymentPaidFirmIdsDto, int[]>(
            uri, criteria, setting: BuildHttpQuerySetting(setting), cancellationToken: cancellationToken);
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
            uri, criteriaDto, setting: BuildHttpQuerySetting(setting), cancellationToken: cancellationToken);
    }

    protected override string GetApiEndpoint()
    {
        return apiEndpoint.Value;
    }
    
    private static InfrastrcutureV2HttpQuerySetting BuildHttpQuerySetting(
        HttpQuerySetting setting)
    {
        return setting == null
            ? null
            : new InfrastrcutureV2HttpQuerySetting
            {
                DontThrowOn404 = setting.DontThrowOn404,
                Timeout = setting.Timeout,
            };
    }
}
