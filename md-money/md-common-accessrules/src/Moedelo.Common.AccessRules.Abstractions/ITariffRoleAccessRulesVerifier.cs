using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;

namespace Moedelo.Common.AccessRules.Abstractions
{
    public interface ITariffRoleAccessRulesVerifier
    {
        Task<bool> HasAllRuleAsync(RoleId roleId, TariffId tariffId, IReadOnlyCollection<TariffId> additionalTariffIds, params AccessRule[] accessRules);

        Task<bool> HasAnyRuleAsync(RoleId roleId, TariffId tariffId, IReadOnlyCollection<TariffId> additionalTariffIds, params AccessRule[] accessRules);
    }
}