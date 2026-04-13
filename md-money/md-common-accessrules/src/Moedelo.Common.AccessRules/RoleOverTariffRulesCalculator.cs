using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.AccessRules
{
    [InjectAsSingleton(typeof(IRoleOverTariffRulesCalculator))]
    internal sealed class RoleOverTariffRulesCalculator : IRoleOverTariffRulesCalculator
    {
        private readonly IAccessRulesCache accessRulesCache;

        public RoleOverTariffRulesCalculator(IAccessRulesCache accessRulesCache)
        {
            this.accessRulesCache = accessRulesCache;
        }

        public async Task<HashSet<AccessRule>> CalculateRulesAsync(
            int roleId,
            IReadOnlyCollection<int> tariffIds)
        {
            var tariffsRulesTask = accessRulesCache.GetTariffsRulesAsync(tariffIds);

            var roleRules = await accessRulesCache.GetRoleRulesAsync(roleId).ConfigureAwait(false);
            var tariffsRules = await tariffsRulesTask.ConfigureAwait(false);
            
            return new HashSet<AccessRule>(roleRules.Intersect(tariffsRules));
        }
    }
}