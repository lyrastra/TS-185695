using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.Outsource.Dto;
using Moedelo.Workflow.Dto.Users;

namespace Moedelo.Workflow.Client.Users;

[InjectAsSingleton(typeof(IWorkflowUserApiClient))]
internal sealed class WorkflowUserApiClient : BaseCoreApiClient, IWorkflowUserApiClient
{
    private static readonly HttpQuerySetting DontThrowOn404 = new HttpQuerySetting
    {
        DontThrowOn404 = true
    };

    private readonly SettingValue apiEndpoint;

    public WorkflowUserApiClient(
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
        apiEndpoint = settingRepository.Get("WorkflowApiEndpoint");
    }

    protected override string GetApiEndpoint()
    {
        return apiEndpoint.Value;
    }

    public async Task<WorkflowUserDto> GetByUserIdAsync(int firmId, int userId, CancellationToken cancellationToken)
    {
        var uri = $"/v1/users/userId/{userId}";

        var headers = await GetPrivateTokenHeaders(firmId, userId, cancellationToken)
            .ConfigureAwait(false);

        var response = await GetAsync<ApiDataResponseDto<WorkflowUserDto>>(
                uri, queryHeaders: headers, setting: DontThrowOn404, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return response?.data;
    }

    public async Task<WorkflowUserDto> CreateAsync(WorkflowUserDto model)
    {
        const string uri = "/v1/users";

        var headers = await GetUnidentifiedTokenHeaders().ConfigureAwait(false);
        var response = await PostAsync<WorkflowUserDto, ApiDataResponseDto<WorkflowUserDto>>(uri, model, queryHeaders: headers)
            .ConfigureAwait(false);

        return response.data;
    }
}