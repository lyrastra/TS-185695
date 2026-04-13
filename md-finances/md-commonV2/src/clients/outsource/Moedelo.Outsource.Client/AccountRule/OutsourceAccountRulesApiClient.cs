using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.Outsource.Dto;
using Moedelo.Outsource.Dto.AccountRule;

namespace Moedelo.Outsource.Client.AccountRule;

[InjectAsSingleton(typeof(IOutsourceAccountRulesApiClient))]
public class OutsourceAccountRulesApiClient : BaseCoreApiClient, IOutsourceAccountRulesApiClient
{
    private readonly SettingValue apiEndpoint;

    public OutsourceAccountRulesApiClient(
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

    public async Task AddRulesAsync(int firmId, int userId, int accountId, AccountRulesPutDto dto)
    {
        var headers = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
        await PutAsync<AccountRulesPutDto, ApiDataResponseDto<AccountRuleDto>>($"/v1/AccountRules/{accountId}", dto, headers).ConfigureAwait(false);
    }
}