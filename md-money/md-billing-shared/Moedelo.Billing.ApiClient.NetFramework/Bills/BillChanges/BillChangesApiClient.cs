using System;
using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.BillChanges;
using Moedelo.Billing.Abstractions.BillChanges.Dto;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Billing.ApiClient.NetFramework.Bills.BillChanges;

[InjectAsSingleton(typeof(IBillChangesApiClient))]
public class BillChangesApiClient : BaseApiClient, IBillChangesApiClient
{
    private const string Prefix = "v1/management/";

    private readonly SettingValue apiEndpoint;

    public BillChangesApiClient(
        IHttpRequestExecutor httpRequestExecutor,
        IUriCreator uriCreator,
        IResponseParser responseParser,
        IAuditTracer auditTracer,
        IAuditScopeManager auditScopeManager,
        ISettingRepository settingRepository)
        : base(
            httpRequestExecutor,
            uriCreator,
            responseParser,
            auditTracer,
            auditScopeManager)
    {
        apiEndpoint = settingRepository.Get("BillingBillsApiEndpoint");
    }

    public Task<BillsCanChangeResponseDto> CanChangeBillLegalTypeAsync(BillsChangeLegalTypeRequestDto requestDto)
    {
        const string route = "LegalType/CanChange";

        return PostAsync<BillsChangeLegalTypeRequestDto, BillsCanChangeResponseDto>(route, requestDto);
    }

    public Task<BillsCanChangeResponseDto> CanChangeBillTaxationSystemTypeAsync(BillChangeTaxationSystemRequestDto requestDto)
    {
        const string route = "TaxationSystem/CanChange";

        return PostAsync<BillChangeTaxationSystemRequestDto, BillsCanChangeResponseDto>(route, requestDto);
    }

    public Task ChangeBillLegalTypeAsync(BillsChangeLegalTypeRequestDto requestDto)
    {
        const string route = "LegalType/Change";

        return PostAsync<BillsChangeLegalTypeRequestDto, BillsCanChangeResponseDto>(route, requestDto);
    }

    public Task ChangeBillTaxationSystemTypeAsync(BillChangeTaxationSystemRequestDto requestDto)
    {
        const string route = "TaxationSystem/Change";

        return PostAsync<BillChangeTaxationSystemRequestDto, BillsCanChangeResponseDto>(route, requestDto);
    }

    protected override string GetApiEndpoint()
    {
        return $"{apiEndpoint.Value}{Prefix}";
    }
}
