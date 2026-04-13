using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.AccessRules
{
    [InjectAsSingleton(typeof(ITariffRoleAccessRulesVerifier))]
    internal sealed class TariffRoleAccessRulesVerifier : ITariffRoleAccessRulesVerifier
    {
        private readonly IRoleOverTariffRulesCalculator rulesCalculator;

        public TariffRoleAccessRulesVerifier(IRoleOverTariffRulesCalculator rulesCalculator)
        {
            this.rulesCalculator = rulesCalculator;
        }

        public async Task<bool> HasAllRuleAsync(
            RoleId roleId,
            TariffId tariffId,
            IReadOnlyCollection<TariffId> additionalTariffIds,
            params AccessRule[] accessRules)
        {
            if (!Validate(roleId, tariffId))
            {
                return false;
            }

            var rules = await GetRulesAsync(roleId, tariffId, additionalTariffIds).ConfigureAwait(false);

            return accessRules.All(rule => rules.Contains(rule));
        }

        public async Task<bool> HasAnyRuleAsync(
            RoleId roleId,
            TariffId tariffId,
            IReadOnlyCollection<TariffId> additionalTariffIds,
            params AccessRule[] accessRules)
        {
            if (!Validate(roleId, tariffId))
            {
                return false;
            }

            var rules = await GetRulesAsync(roleId, tariffId, additionalTariffIds).ConfigureAwait(false);

            return accessRules.Any(rule => rules.Contains(rule));
        }

        private static bool Validate(RoleId roleId, TariffId tariffId)
        {
            return roleId != RoleId.Unidentified && tariffId != TariffId.Unidentified;
        }

        private Task<HashSet<AccessRule>> GetRulesAsync(
            RoleId roleId,
            TariffId tariffId,
            IReadOnlyCollection<TariffId> additionalTariffIds)
        {
            var tariffIds = new[] {tariffId}
                .Concat(additionalTariffIds)
                .Select(id => (int) id)
                .ToArray();

            return rulesCalculator.CalculateRulesAsync((int) roleId, tariffIds);
        }
    }
}