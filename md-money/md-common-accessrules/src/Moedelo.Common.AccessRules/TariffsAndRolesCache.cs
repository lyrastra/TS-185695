using System.Linq;
using System.Threading.Tasks;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.AccessRules
{
    [InjectAsSingleton(typeof(ITariffsAndRolesCache))]
    internal sealed class TariffsAndRolesCache : ITariffsAndRolesCache
    {
        private readonly IAccessRuleCacheOutdateChecker outdateChecker;
        private readonly AccountApiClient accountApiClient;
        
        private volatile TariffsAndRoles сacheTariffsAndRoles;

        public TariffsAndRolesCache(
            IAccessRuleCacheOutdateChecker outdateChecker,
            AccountApiClient accountApiClient)
        {
            this.outdateChecker = outdateChecker;
            this.accountApiClient = accountApiClient;
        }

        public async Task<TariffsAndRoles> GetTariffsAndRolesAsync()
        {
            if (await outdateChecker.IsOutdatedAsync().ConfigureAwait(false))
            {
                Invalidate();
            }

            var сacheTariffsAndRolesRef = сacheTariffsAndRoles;

            if (сacheTariffsAndRolesRef != null)
            {
                // иногда мы вернём старое значение, но никогда null
                return сacheTariffsAndRolesRef;
            }

            сacheTariffsAndRolesRef = await CreateRulesMapsAsync().ConfigureAwait(false);
            сacheTariffsAndRoles = сacheTariffsAndRolesRef;
            outdateChecker.OnCacheRefreshed();
            
            return сacheTariffsAndRolesRef;
        }

        public void Invalidate()
        {
            сacheTariffsAndRoles = null;
        }

        private async Task<TariffsAndRoles> CreateRulesMapsAsync()
        {
            var tariffsAndRolesDto = await accountApiClient.GetTariffsAndRolesAsync().ConfigureAwait(false);
            
            return new TariffsAndRoles
            {
                Tariffs = tariffsAndRolesDto.Tariffs.ToDictionary(x => x.Id),
                Roles = tariffsAndRolesDto.RoleInfos.ToDictionary(x => x.Id)
            };
        }
    }
}