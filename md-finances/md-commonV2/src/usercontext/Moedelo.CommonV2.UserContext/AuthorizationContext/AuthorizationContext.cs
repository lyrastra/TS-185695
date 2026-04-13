using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Access;
using Moedelo.CommonV2.UserContext.Domain.AuthorizationContext;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Context;

namespace Moedelo.CommonV2.UserContext.AuthorizationContext;

[InjectPerWebRequest(typeof(IAuthorizationContext))]
public class AuthorizationContext : IAuthorizationContext
{
    private readonly IAuditContext auditContext;
    private readonly IAuthorizationContextDataCachingReader dataReader;
    private Lazy<Task<IAuthorizationContextData>> lazyDataTask;

    public AuthorizationContext(
        IAuditContext auditContext,
        IAuthorizationContextDataCachingReader dataReader)
    {
        this.auditContext = auditContext;
        this.dataReader = dataReader;
        Invalidate();
    }

    public int FirmId => auditContext.FirmId ?? 0;

    public int UserId => auditContext.UserId ?? 0;

    private bool IsAuthenticated => UserId > 0 || FirmId > 0;
        
    // на самом деле пользователь авторизован, когда для него определена роль в фирме.
    // Но исторически здесь используется такая логика (которая проверяет только аутентификацию)
    public bool IsAuthorized => IsAuthenticated && GetRoleIdAsync().Result > 0;

    public async Task<int> GetRoleIdAsync()
    {
        var data = await GetDataAsync().ConfigureAwait(false);

        return data.RoleId;
    }

    public async Task<bool> HasRuleAsync(AccessRule accessRule)
    {
        var data = await GetDataAsync().ConfigureAwait(false);
        var roleRules = data.UserRules;

        return roleRules.Contains(accessRule);
    }

    public async Task<bool> HasAllRuleAsync(params AccessRule[] accessRules)
    {
        AssertNotEmpty(accessRules);
        var data = await GetDataAsync().ConfigureAwait(false);
        var roleRules = data.UserRules;

        return accessRules.All(r => roleRules.Contains(r));
    }

    public async Task<bool> HasAnyRuleAsync(params AccessRule[] accessRules)
    {
        AssertNotEmpty(accessRules);
        var data = await GetDataAsync().ConfigureAwait(false);
        var roleRules = data.UserRules;

        return accessRules.Any(r => roleRules.Contains(r));
    }

    public async Task<bool> HasAnyRuleInTariffAsync(params AccessRule[] accessRules)
    {
        AssertNotEmpty(accessRules);
        var data = await GetDataAsync().ConfigureAwait(false);
        var tariffRules = data.TariffRules;

        return accessRules.Any(r => tariffRules.Contains(r));
    }

    public async Task<List<AccessRule>> GetGrantedRulesAsync(params AccessRule[] rulesToCheck)
    {
        AssertNotEmpty(rulesToCheck);
        var data = await GetDataAsync().ConfigureAwait(false);
        var userRules = data.UserRules;

        return rulesToCheck.Intersect(userRules).ToList();
    }
        
    public async Task<IReadOnlyCollection<AccessRule>> GetUserRulesAsync()
    {
        var data = await GetDataAsync().ConfigureAwait(false);
        return data.UserRules;
    }
        
    public void Invalidate()
    {
        lazyDataTask = new Lazy<Task<IAuthorizationContextData>>(() => IsAuthenticated
            ? dataReader.GetAsync(FirmId, UserId)
            : Task.FromResult(dataReader.GetEmpty()));
    }

    private async Task<IAuthorizationContextData> GetDataAsync()
    {
        try
        {
            return await lazyDataTask.Value.ConfigureAwait(false);
        }
        catch
        {
            Invalidate();
            throw;
        }
    }

    private static void AssertNotEmpty(IReadOnlyCollection<AccessRule> accessRules)
    {
        if (accessRules?.Any() != true)
        {
            throw new ArgumentException(nameof(accessRules), $"Empty {nameof(accessRules)} value (={accessRules}) is not allowed here");
        }
    }
}