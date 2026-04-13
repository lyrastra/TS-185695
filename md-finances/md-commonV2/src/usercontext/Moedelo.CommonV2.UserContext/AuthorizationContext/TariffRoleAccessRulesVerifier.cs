using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Access;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.CommonV2.UserContext.Domain.AuthorizationContext;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;

namespace Moedelo.CommonV2.UserContext.AuthorizationContext;
#pragma warning disable CS0618
[InjectAsSingleton(typeof(ITariffRoleAccessRulesVerifier))]
public class TariffRoleAccessRulesVerifier : ITariffRoleAccessRulesVerifier
#pragma warning restore CS0618
{
    private readonly IAccessRulesCache accessRulesCache;

    public TariffRoleAccessRulesVerifier(IAccessRulesCache accessRulesCache)
    {
        this.accessRulesCache = accessRulesCache;
    }

    public async Task<bool> HasAllRuleAsync(TariffRolePair tariffRolePair, AccessRule[] accessRules)
    {
        var rules = await GetTariffRoleRulesAsync(tariffRolePair).ConfigureAwait(false);

        return accessRules.All(r => rules.Contains(r));
    }

    public async Task<bool> HasAnyRuleAsync(TariffRolePair tariffRolePair, AccessRule[] accessRules)
    {
        var rules = await GetTariffRoleRulesAsync(tariffRolePair).ConfigureAwait(false);

        return accessRules.Any(r => rules.Contains(r));
    }

    public async Task<bool> HasAnyRuleInTariffAsync(int tariffId, AccessRule[] accessRules)
    {
        var tariffRules = await accessRulesCache.GetTariffRulesAsync(tariffId).ConfigureAwait(false);
        return accessRules.Any(r => tariffRules.Contains(r));
    }

    public async Task<HashSet<AccessRule>> GetTariffRoleRulesAsync(TariffRolePair tariffRolePair)
    {
        var tariffIdList = new HashSet<int> {tariffRolePair.TariffId};

        foreach (var additionalTariffId in tariffRolePair.AdditionalTariffIds)
        {
            tariffIdList.Add(additionalTariffId);
        }

        var roleRulesTask = accessRulesCache.GetRoleRulesAsync(tariffRolePair.RoleId);
        var tariffRulesTask = accessRulesCache.GetTariffsRulesAsync(tariffIdList);

        await Task.WhenAll(roleRulesTask, tariffRulesTask).ConfigureAwait(false);

        var roleRules = roleRulesTask.Result;
        var tariffRules = tariffRulesTask.Result;

        return new HashSet<AccessRule>(roleRules.Intersect(tariffRules));
    }
}