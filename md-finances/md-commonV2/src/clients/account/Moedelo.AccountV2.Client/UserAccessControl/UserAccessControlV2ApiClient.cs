using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountV2.Dto.UserAccessControl;
using Moedelo.Common.Enums.Enums.Access;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountV2.Client.UserAccessControl;

[InjectAsSingleton(typeof(IUserAccessControlV2ApiClient))]
public class UserAccessControlV2ApiClient : BaseApiClient, IUserAccessControlV2ApiClient
{
    private readonly SettingValue apiEndPoint;

    public UserAccessControlV2ApiClient(
        IHttpRequestExecutor httpRequestExecutor, 
        IUriCreator uriCreator, 
        IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,  
        ISettingRepository settingRepository)
        : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
    {
        apiEndPoint = settingRepository.Get("UserAccessControlApiEnpoint");
    }

    protected override string GetApiEndpoint()
    {
        return apiEndPoint.Value;
    }

    public async Task<ISet<AccessRule>> GetAccessRulesAsync(int firmId, int userId,
        CancellationToken cancellationToken)
    {
        var response = await GetAsync<DataWrapper<ISet<AccessRule>>>(
            "/GetAccessRules",
            new { firmId, userId }, cancellationToken: cancellationToken).ConfigureAwait(false);

        return response.Data;
    }

    public async Task<ISet<AccessRule>> GetExplicitUserRulesAsync(int userId, CancellationToken cancellationToken)
    {
        const string uri = "/GetExplicitUserRules";
        var queryParams = new { userId }; 

        var response = await GetAsync<DataWrapper<ISet<AccessRule>>>(
                uri, queryParams, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return response.Data;
    }

    public async Task<List<UserAccountPermissions>> GetAccountPermissionsAsync(int userId,
        IReadOnlyCollection<int> userIds, CancellationToken cancellationToken)
    {
        userIds = userIds.AsSet();
        var uri = $"/GetAccountPermissions?userId={userId}";
        var dataWrapper = await PostAsync<IReadOnlyCollection<int>, DataWrapper<List<UserAccountPermissions>>>(
                uri, userIds, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return dataWrapper.Data;
    }

    public async Task<bool> IsLostAccountAsync(int userId, CancellationToken cancellationToken)
    {
        var uri = $"/IsLostAccount?userId={userId}";
        var response = await GetAsync<DataWrapper<bool>>(uri, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return response.Data;
    }

    public async Task<ISet<AccessRule>> GetForUserAsync(int userId, CancellationToken cancellationToken)
    {
        var uri = $"/GetForUser?userId={userId}";
        var data = await GetAsync<DataWrapper<ISet<AccessRule>>>(uri, cancellationToken: cancellationToken).ConfigureAwait(false);

        return data.Data;
    }

    public async Task<int> GetMainUserIdAsync(int firmId, CancellationToken cancellationToken)
    {
        var uri = $"/GetMainUserId?firmId={firmId}";
        var data = await GetAsync<DataWrapper<int>>(uri, cancellationToken: cancellationToken).ConfigureAwait(false);

        return data.Data;
    }
}