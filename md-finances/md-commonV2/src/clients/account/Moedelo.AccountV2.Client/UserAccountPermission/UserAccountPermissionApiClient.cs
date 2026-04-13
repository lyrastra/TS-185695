using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountV2.Dto.UserAccountPermission;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountV2.Client.UserAccountPermission;

[InjectAsSingleton(typeof(IUserAccountPermissionApiClient))]
public class UserAccountPermissionApiClient : BaseApiClient, IUserAccountPermissionApiClient
{
    private readonly SettingValue apiEndPoint;

    public UserAccountPermissionApiClient(
        IHttpRequestExecutor httpRequestExecutor,
        IUriCreator uriCreator,
        IResponseParser responseParser,
        ISettingRepository settingRepository,
        IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
        : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
    {
        apiEndPoint = settingRepository.Get("AccountPrivateApiEndpoint");
    }

    public Task DeleteAsync(IReadOnlyCollection<int> userIds)
    {
        userIds = userIds.AsSet();
        return PostAsync("/V2/Delete", userIds);
    }

    public Task<bool> IsAdminAsync(int userId, CancellationToken cancellationToken)
    {
        return GetAsync<bool>($"/V2/IsAdmin?userId={userId}", cancellationToken: cancellationToken);
    }

    public Task SaveAsync(int firmId, int userId, IReadOnlyCollection<UserAccountPermissionDto> dto)
    {
        return PostAsync($"/Save?firmId={firmId}&userId={userId}", dto);
    }

    protected override string GetApiEndpoint()
    {
        return apiEndPoint.Value + "/Rest/UserAccountPermission";
    }
}