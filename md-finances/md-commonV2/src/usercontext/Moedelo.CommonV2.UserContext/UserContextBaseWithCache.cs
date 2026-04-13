using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Access;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.CommonV2.UserContext.Domain.AuthorizationContext;
using Moedelo.CommonV2.UserContext.Domain.BillingContext;
using Moedelo.CommonV2.UserContext.Domain.ExtraData;
using Moedelo.InfrastructureV2.Domain.Interfaces.Context;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;

namespace Moedelo.CommonV2.UserContext;

public abstract class UserContextBaseWithCache : IUserContext
{
    private readonly string TAG;

    private readonly ILogger logger;
    private readonly IAuditContext auditContext;
    private readonly IAuthorizationContext authorizationContext;
    private readonly IFirmBillingContext billingContext;
    private readonly IUserContextExtraDataLoader userContextExtraDataLoader;
    private Lazy<Task<IUserContextExtraData>> lazyLoadedUserContextDataTask;

    protected UserContextBaseWithCache(IUserContextBaseWithCacheDependencies deps)
    {
        TAG = GetType().Name;
        this.logger = deps.Logger;
        this.auditContext = deps.AuditContext;
        this.authorizationContext = deps.AuthorizationContext;
        this.billingContext = deps.BillingContext;
        this.userContextExtraDataLoader = deps.UserContextExtraDataLoader;
        InvalidateUserContextData();
    }

    public IAuditContext GetAuditContext()
    {
        return auditContext;
    }

    public int FirmId => authorizationContext.FirmId;

    public int UserId => authorizationContext.UserId;

    public bool IsAuthorized => authorizationContext.IsAuthorized;

    public Task<int> GetRoleIdAsync()
    {
        return authorizationContext.GetRoleIdAsync();
    }

    public Task<IFirmBillingContextData> GetBillingContextDataAsync()
    {
        return billingContext.GetDataAsync();
    }

    public async Task<IUserContextExtraData> GetContextExtraDataAsync()
    {
        try
        {
            var data = await lazyLoadedUserContextDataTask.Value.ConfigureAwait(false);

            return data;
        }
        catch
        {
            InvalidateUserContextData();
            throw;
        }
    }

    private async Task<IUserContextExtraData> GetUserContextDataAsync()
    {
        try
        {
            if (!authorizationContext.IsAuthenticated())
            {
                return userContextExtraDataLoader.GetUnauthorized();
            }

            var roleId = await authorizationContext.GetRoleIdAsync().ConfigureAwait(false);

            return await userContextExtraDataLoader.GetAsync(FirmId, UserId, roleId).ConfigureAwait(false)
                   ?? userContextExtraDataLoader.GetUnauthorized();
        }
        catch (Exception ex)
        {
            logger.Error(
                TAG,
                "Caught exception at attempt to load user context data ",
                ex,
                extraData: auditContext);
            throw;
        }
    }

    public void Invalidate()
    {
        authorizationContext.Invalidate();
        billingContext.Invalidate();
        InvalidateUserContextData();
    }

    public Task<bool> HasAllRuleAsync(params AccessRule[] checkRules)
    {
        return authorizationContext.HasAllRuleAsync(checkRules);
    }

    public Task<bool> HasAllRuleAsync(AccessRule accessRule)
    {
        return authorizationContext.HasRuleAsync(accessRule);
    }

    public Task<bool> HasAnyRuleAsync(params AccessRule[] checkRules)
    {
        return authorizationContext.HasAnyRuleAsync(checkRules);
    }

    public Task<bool> HasAnyRuleAsync(AccessRule accessRule)
    {
        return authorizationContext.HasRuleAsync(accessRule);
    }

    public Task<bool> HasAnyTariffRuleAsync(params AccessRule[] checkRules)
    {
        return authorizationContext.HasAnyRuleInTariffAsync(checkRules);
    }

    public Task<List<AccessRule>> GetGrantedRulesAsync(params AccessRule[] rulesToCheck)
    {
        return authorizationContext.GetGrantedRulesAsync(rulesToCheck);
    }

    public Task<IReadOnlyCollection<AccessRule>> GetUserRulesAsync()
    {
        return authorizationContext.GetUserRulesAsync();
    }

    private void InvalidateUserContextData()
    {
        lazyLoadedUserContextDataTask = new Lazy<Task<IUserContextExtraData>>(GetUserContextDataAsync);
    }
}