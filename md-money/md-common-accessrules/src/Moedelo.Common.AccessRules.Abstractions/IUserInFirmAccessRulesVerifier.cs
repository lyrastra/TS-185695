using System.Threading.Tasks;
using Moedelo.Common.Types;

namespace Moedelo.Common.AccessRules.Abstractions
{
    public interface IUserInFirmAccessRulesVerifier
    {
        Task<bool> HasAllRuleAsync(FirmId firmId, UserId userId, params AccessRule[] accessRules);
        Task<bool> HasAllRuleAsync(FirmId firmId, UserId userId, AccessRule accessRule);

        Task<bool> HasAnyRuleAsync(FirmId firmId, UserId userId, params AccessRule[] accessRules);
        Task<bool> HasAnyRuleAsync(FirmId firmId, UserId userId, AccessRule accessRule);
    }
}