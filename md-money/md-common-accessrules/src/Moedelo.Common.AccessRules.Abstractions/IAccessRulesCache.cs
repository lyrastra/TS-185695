using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Common.AccessRules.Abstractions
{
    public interface IAccessRulesCache
    {
        Task<HashSet<AccessRule>> GetRoleRulesAsync(int roleId);

        Task<HashSet<AccessRule>> GetTariffRulesAsync(int tariffId);

        Task<HashSet<AccessRule>> GetTariffsRulesAsync(IReadOnlyCollection<int> tariffIdList);
    }
}