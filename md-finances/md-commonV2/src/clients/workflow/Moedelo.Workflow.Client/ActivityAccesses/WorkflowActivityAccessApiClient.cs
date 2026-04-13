using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.Outsource.Dto;
using Moedelo.Workflow.Dto.ActivityAccesses;
using System.Threading.Tasks;

namespace Moedelo.Workflow.Client.ActivityAccesses;

[InjectAsSingleton(typeof(IWorkflowActivityAccessApiClient))]
internal sealed class WorkflowActivityAccessApiClient : BaseCoreApiClient, IWorkflowActivityAccessApiClient
{
    private readonly SettingValue apiEndpoint;

    public WorkflowActivityAccessApiClient(
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

    public async Task<ActivityAccessDto[]> CreateAsync(int accountId, string[] activityKeys)
    {
        var uri = $"/v1/activities/accesses?accountId={accountId}";

        var headers = await GetUnidentifiedTokenHeaders().ConfigureAwait(false);
        var response = await PostAsync<string[], ApiDataResponseDto<ActivityAccessDto[]>>(uri, activityKeys, queryHeaders: headers)
            .ConfigureAwait(false);

        return response.data;
    }
}