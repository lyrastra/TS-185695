using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.Outsource.Dto;
using Moedelo.Outsource.Dto.Accounts;

namespace Moedelo.Outsource.Client.Accounts;

[InjectAsSingleton(typeof(IOutsourceAccountsApiClient))]
internal sealed class OutsourceAccountsApiClient : BaseCoreApiClient, IOutsourceAccountsApiClient
{
    private readonly SettingValue apiEndpoint;

    public OutsourceAccountsApiClient(
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
        apiEndpoint = settingRepository.Get("OutsourceAccountApiEndpoint");
    }

    protected override string GetApiEndpoint()
    {
        return apiEndpoint.Value;
    }
        
    public async Task<int> CreateAsync(int firmId, int userId, AccountPostDto dto)
    {
        var headers = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
        var response = await PostAsync<AccountPostDto, ApiDataResponseDto<IdValue<int>>>($"/v1", dto, headers).ConfigureAwait(false);

        return response.data.Id;
    }
}