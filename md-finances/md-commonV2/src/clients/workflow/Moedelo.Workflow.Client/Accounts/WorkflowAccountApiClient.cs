using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.Outsource.Dto;
using Moedelo.Workflow.Dto.Accounts;
using System.Threading.Tasks;

namespace Moedelo.Workflow.Client.Accounts;

[InjectAsSingleton(typeof(IWorkflowAccountApiClient))]
internal sealed class WorkflowAccountApiClient : BaseCoreApiClient, IWorkflowAccountApiClient
{
    private readonly SettingValue apiEndpoint;

    public WorkflowAccountApiClient(
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

    public async Task<AccountDto> CreateAsync(AccountDto model)
    {
        var uri = $"/v1/accounts";

        var headers = await GetUnidentifiedTokenHeaders().ConfigureAwait(false);
        var response = await PostAsync<AccountDto, ApiDataResponseDto<AccountDto>>(uri, model, queryHeaders: headers)
            .ConfigureAwait(false);

        return response.data;
    }
}