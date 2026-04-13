using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Billing.Abstractions.BillChanges;
using Moedelo.Billing.Abstractions.BillChanges.Dto;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Billing.Clients.BillChanges;

[InjectAsSingleton(typeof(IBillChangesApiClient))]
public class BillChangesApiClient : BaseApiClient, IBillChangesApiClient
{
    private const string prefix = "v1/management/";

    public BillChangesApiClient(
        IHttpRequestExecuter httpRequestExecutor,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuthHeadersGetter authHeadersGetter,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<BillChangesApiClient> logger)
        : base(
            httpRequestExecutor,
            uriCreator,
            auditTracer,
            authHeadersGetter,
            auditHeadersGetter,
            settingRepository.Get("BillingBillsApiEndpoint"),
            logger)
    {
    }

    public Task<BillsCanChangeResponseDto> CanChangeBillLegalTypeAsync(
        BillsChangeLegalTypeRequestDto requestDto)
    {
        const string route = "LegalType/CanChange";

        var uri = $"{prefix}{route}";

        return PostAsync<BillsChangeLegalTypeRequestDto, BillsCanChangeResponseDto>(uri, requestDto);
    }

    public Task<BillsCanChangeResponseDto> CanChangeBillTaxationSystemTypeAsync(
        BillChangeTaxationSystemRequestDto requestDto)
    {
        const string route = "TaxationSystem/CanChange";

        var uri = $"{prefix}{route}";

        return PostAsync<BillChangeTaxationSystemRequestDto, BillsCanChangeResponseDto>(uri, requestDto);
    }

    public Task ChangeBillLegalTypeAsync(BillsChangeLegalTypeRequestDto requestDto)
    {
        const string route = "LegalType/Change";

        var uri = $"{prefix}{route}";

        return PostAsync<BillsChangeLegalTypeRequestDto, BillsCanChangeResponseDto>(uri, requestDto);
    }

    public Task ChangeBillTaxationSystemTypeAsync(BillChangeTaxationSystemRequestDto requestDto)
    {
        const string route = "TaxationSystem/Change";

        var uri = $"{prefix}{route}";

        return PostAsync<BillChangeTaxationSystemRequestDto, BillsCanChangeResponseDto>(uri, requestDto);
    }
}
