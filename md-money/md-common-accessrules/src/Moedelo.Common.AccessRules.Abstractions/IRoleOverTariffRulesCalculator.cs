using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Common.AccessRules.Abstractions
{
    public interface IRoleOverTariffRulesCalculator
    {
        Task<HashSet<AccessRule>> CalculateRulesAsync(int roleId, IReadOnlyCollection<int> tariffIds);
    }
}