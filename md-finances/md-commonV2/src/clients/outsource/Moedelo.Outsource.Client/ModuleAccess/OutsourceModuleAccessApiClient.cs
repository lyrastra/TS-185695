using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.Outsource.Dto;
using Moedelo.Outsource.Dto.ModuleAccess;

namespace Moedelo.Outsource.Client.ModuleAccess;

[InjectAsSingleton(typeof(IOutsourceModuleAccessApiClient))]
internal sealed class OutsourceModuleAccessApiClient : BaseCoreApiClient, IOutsourceModuleAccessApiClient
{
    private readonly SettingValue apiEndpoint;

    public OutsourceModuleAccessApiClient(
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

    public async Task<IReadOnlyList<ModuleAccessDto>> GetAsync(ModuleGetDto dto, CancellationToken cancellationToken)
    {
        var response = await GetAsync<ApiDataResponseDto<ModuleAccessDto[]>>(
                "/v1/ModuleAccess", dto, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return response?.data;
    }

    public Task EnableAsync(int accountId, ModuleType type)
    {
        return PostAsync($"/v1/ModuleAccess/{accountId}/{type}");
    }

    public Task DisableAsync(int accountId, ModuleType type)
    {
        return DeleteAsync($"/v1/ModuleAccess/{accountId}/{type}");
    }

    public async Task<IReadOnlyList<ModuleAccessDto>> GetByTypesAsync(int accountId, ModuleType[] types, CancellationToken cancellationToken = default)
    {
        var url = $"/v1/ModuleAccess/byTypes?accountId={accountId}";
        
        var response = await PostAsync<ModuleType[], ApiDataResponseDto<ModuleAccessDto[]>>(url, types, cancellationToken: cancellationToken);
        
        return response?.data;
    }
}