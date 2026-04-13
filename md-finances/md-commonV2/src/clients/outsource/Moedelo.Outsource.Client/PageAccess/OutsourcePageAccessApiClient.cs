using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.Outsource.Dto.PageAccess;

namespace Moedelo.Outsource.Client.PageAccess;

[InjectAsSingleton(typeof(IOutsourcePageAccessApiClient))]
internal sealed class OutsourcePageAccessApiClient : BaseCoreApiClient, IOutsourcePageAccessApiClient
{
    private readonly SettingValue apiEndpoint;

    public OutsourcePageAccessApiClient(
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
        apiEndpoint = settingRepository.Get("OutsourcePageAccessApiEndpoint");
    }

    protected override string GetApiEndpoint()
    {
        return apiEndpoint.Value;
    }

    public Task UpdateAsync(GroupSettingPostDto dto)
    {
        return PostAsync($"/v1/settings", dto);
    }
}