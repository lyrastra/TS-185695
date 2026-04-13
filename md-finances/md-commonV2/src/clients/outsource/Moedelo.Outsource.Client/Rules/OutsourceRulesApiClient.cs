using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System.Threading.Tasks;
using Moedelo.Outsource.Dto;

namespace Moedelo.Outsource.Client.Rules;

[InjectAsSingleton(typeof(IOutsourceRulesApiClient))]
internal sealed class OutsourceRulesApiClient : BaseCoreApiClient, IOutsourceRulesApiClient
{
    private readonly SettingValue apiEndpoint;

    public OutsourceRulesApiClient(
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

    public async Task<List<string>> GetRulesListAsync(int accountId, int roleId, int firmId, int userId, CancellationToken cancellationToken)
    {
        var uri = $"/v1/rules?accountId={accountId}&roleId={roleId}";
        var headers = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
        var response = await GetAsync<ApiDataResponseDto<string[]>>(uri, queryHeaders: headers, cancellationToken: cancellationToken).ConfigureAwait(false);

        return response?.data.ToList();
    }
}