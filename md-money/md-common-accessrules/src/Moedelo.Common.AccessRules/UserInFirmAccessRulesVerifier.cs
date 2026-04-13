using System.Linq;
using System.Threading.Tasks;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.AccessRules
{
    [InjectAsSingleton(typeof(IUserInFirmAccessRulesVerifier))]
    internal sealed class UserInFirmAccessRulesVerifier : IUserInFirmAccessRulesVerifier
    {
        private readonly IAuthorizationContextDataCachingReader authDataReader;
        
        public UserInFirmAccessRulesVerifier(
            IAuthorizationContextDataCachingReader authDataReader)
        {
            this.authDataReader = authDataReader;
        }

        public async Task<bool> HasAllRuleAsync(FirmId firmId, UserId userId, params AccessRule[] accessRules)
        {
            if (!Validate(firmId, userId))
            {
                return false;
            }

            var authData = await authDataReader.GetAsync(firmId, userId).ConfigureAwait(false);
            
            return accessRules.All(rule => authData.RoleRules.Contains(rule));
        }

        public Task<bool> HasAllRuleAsync(FirmId firmId, UserId userId, AccessRule accessRule)
        {
            // для одного правила нет разница между has any и has all
            return HasAnyRuleAsync(firmId, userId, accessRule);
        }

        public async Task<bool> HasAnyRuleAsync(FirmId firmId, UserId userId, params AccessRule[] accessRules)
        {
            if (!Validate(firmId, userId))
            {
                return false;
            }

            var authData = await authDataReader.GetAsync(firmId, userId).ConfigureAwait(false);

            return accessRules.Any(rule => authData.RoleRules.Contains(rule));
        }

        public async Task<bool> HasAnyRuleAsync(FirmId firmId, UserId userId, AccessRule accessRule)
        {
            if (!Validate(firmId, userId))
            {
                return false;
            }

            var authData = await authDataReader.GetAsync(firmId, userId).ConfigureAwait(false);

            return authData.RoleRules.Contains(accessRule);
        }

        private static bool Validate(FirmId firmId, UserId userId)
        {
            return firmId != FirmId.Unidentified && userId != UserId.Unidentified;
        }
    }
}