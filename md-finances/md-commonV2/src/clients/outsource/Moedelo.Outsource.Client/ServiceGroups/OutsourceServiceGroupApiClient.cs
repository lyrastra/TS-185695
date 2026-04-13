using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.Outsource.Dto;
using System.Threading.Tasks;
using Moedelo.Outsource.Dto.ServiceGroups;

namespace Moedelo.Outsource.Client.ServiceGroups;

[InjectAsSingleton(typeof(IOutsourceServiceGroupApiClient))]
internal sealed class OutsourceServiceGroupApiClient : BaseCoreApiClient, IOutsourceServiceGroupApiClient
{
    private readonly SettingValue apiEndpoint;

    public OutsourceServiceGroupApiClient(
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

    public async Task<int> CreateAsync(int accountId, ServiceGroupPostDto data)
    {
        var uri = $"/v1/services/groups?accountId={accountId}";

        var headers = await GetUnidentifiedTokenHeaders().ConfigureAwait(false);
        var response = await PostAsync<ServiceGroupPostDto, ApiDataResponseDto<IdValue<int>>>(uri, data, queryHeaders: headers)
            .ConfigureAwait(false);

        return response.data.Id;
    }

}