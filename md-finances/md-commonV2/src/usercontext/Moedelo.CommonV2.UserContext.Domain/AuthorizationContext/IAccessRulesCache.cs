using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Access;

namespace Moedelo.CommonV2.UserContext.Domain.AuthorizationContext;

public interface IAccessRulesCache
{
    Task<HashSet<AccessRule>> GetRoleRulesAsync(int roleId);
        
    Task<HashSet<AccessRule>> GetTariffRulesAsync(int tariffId);
        
    Task<HashSet<AccessRule>> GetTariffsRulesAsync(IReadOnlyCollection<int> tariffIdList);
}