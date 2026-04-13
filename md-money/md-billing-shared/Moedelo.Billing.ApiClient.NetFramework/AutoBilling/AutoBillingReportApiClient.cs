using System;
using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.AutoBilling;
using Moedelo.Billing.Abstractions.AutoBilling.Dto;
using Moedelo.Billing.Abstractions.AutoBilling.Dto.Report;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Billing.ApiClient.NetFramework.AutoBilling;

[InjectAsSingleton(typeof(IAutoBillingReportApiClient))]
public class AutoBillingReportApiClient : BaseApiClient, IAutoBillingReportApiClient
{
    private readonly SettingValue apiEndPoint;

    public AutoBillingReportApiClient(
        IHttpRequestExecutor httpRequestExecutor,
        IUriCreator uriCreator,
        IResponseParser responseParser,
        ISettingRepository settingRepository,
        IAuditTracer auditTracer,
        IAuditScopeManager auditScopeManager)
        : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
    {
        apiEndPoint = settingRepository.Get("AutoBillingApiEndpoint");
    }

    public Task<AutoBillingReportResponseDto> GetAsync(GetAutoBillingReportRequestDto requestDto)
    {
        const string uri = "/v1/report/get";
        var setting = new HttpQuerySetting(TimeSpan.FromMinutes(2));

        return PostAsync<GetAutoBillingReportRequestDto, AutoBillingReportResponseDto>(uri, requestDto, setting: setting);
    }

    protected override string GetApiEndpoint()
    {
        return apiEndPoint.Value;
    }
}