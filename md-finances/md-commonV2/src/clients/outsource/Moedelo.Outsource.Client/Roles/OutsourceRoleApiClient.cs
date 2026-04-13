using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.Outsource.Dto;
using Moedelo.Outsource.Dto.Roles;

namespace Moedelo.Outsource.Client.Roles;

[InjectAsSingleton(typeof(IOutsourceRoleApiClient))]
internal sealed class OutsourceRoleApiClient : BaseCoreApiClient, IOutsourceRoleApiClient
{
    private readonly SettingValue apiEndpoint;

    public OutsourceRoleApiClient(
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
        apiEndpoint = settingRepository.Get("OutsourceEmployeeApiEndpoint");
    }

    protected override string GetApiEndpoint()
    {
        return apiEndpoint.Value;
    }
        
    public async Task<int> InsertAsync(int firmId, int userId, RolePostDto model)
    {
        var uri = $"/v1/roles";
        var headers = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
        var response = await PostAsync<RolePostDto, ApiDataResponseDto<IdValue<int>>>(uri, model, headers).ConfigureAwait(false);

        return response.data.Id;
    }
}