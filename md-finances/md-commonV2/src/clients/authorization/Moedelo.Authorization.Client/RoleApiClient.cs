using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Authorization.Client.Abstractions;
using Moedelo.Authorization.Dto;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Authorization.Client;

[InjectAsSingleton(typeof(IRoleApiClient))]
internal sealed class RoleApiClient(
    IHttpRequestExecutor httpRequestExecutor,
    IUriCreator uriCreator,
    IResponseParser responseParser,
    ISettingRepository settingRepository,
    IAuditTracer auditTracer,
    IAuditScopeManager auditScopeManager)
    : BaseCoreApiClient(
    httpRequestExecutor,
    uriCreator,
    responseParser,
    settingRepository,
    auditTracer,
    auditScopeManager), IRoleApiClient
{
    private readonly SettingValue endpoint = settingRepository
        .GetRequired("AuthorizationPrivateApiEndpoint");

    protected override string GetApiEndpoint()
    {
        return endpoint.Value;
    }

    public Task<List<RoleDto>> GetRolesAsync()
    {
        return GetAsync<List<RoleDto>>("/v1/Role");
    }
}