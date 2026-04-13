using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.Outsource.Dto;
using Moedelo.Outsource.Dto.Services;

namespace Moedelo.Outsource.Client.Services;

[InjectAsSingleton(typeof(IOutsourceServicesApiClient))]
internal sealed class OutsourceServicesApiClient : BaseCoreApiClient, IOutsourceServicesApiClient
{
    private readonly SettingValue apiEndpoint;

    public OutsourceServicesApiClient(
        IHttpRequestExecutor httpRequestExecutor,
        IUriCreator uriCreator,
        IResponseParser responseParser,
        ISettingRepository settingRepository,
        IAuditTracer auditTracer,
        IAuditScopeManager auditScopeManager)
        : base(
            httpRequestExecutor,
            uriCreator,
            responseParser,
            settingRepository,
            auditTracer,
            auditScopeManager)
    {
        apiEndpoint = settingRepository.Get("OutsourceClientApiEndpoint");
    }

    protected override string GetApiEndpoint()
    {
        return apiEndpoint.Value;
    }

    public async Task<int> CreateAsync(int accountId, ServicePostDto dto)
    {
        var uri = $"/v1/services?accountId={accountId}";
            
        var headers = await GetUnidentifiedTokenHeaders().ConfigureAwait(false);
        var response = await PostAsync<ServicePostDto, ApiDataResponseDto<IdValue<int>>>(uri, dto, queryHeaders: headers)
            .ConfigureAwait(false);
            
        return response.data.Id;
    }
}