using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.AccountV2.Client.TariffsAndRoles;
using Moedelo.Common.Enums.Enums.Access;
using Moedelo.CommonV2.UserContext.Domain.AuthorizationContext;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;

namespace Moedelo.CommonV2.UserContext.AuthorizationContext;

[InjectAsSingleton(typeof(ITariffsAndRolesCache))]
public class TariffsAndRolesCache : ITariffsAndRolesCache
{
    private readonly ITariffsAndRolesApiClient tariffsAndRolesApiClient;
    private readonly ITariffCacheOutdateChecker tariffCacheOutdateChecker;
    private volatile TariffsAndRoles cacheTariffsAndRoles;

    public TariffsAndRolesCache(
        ITariffsAndRolesApiClient tariffsAndRolesApiClient,
        ITariffCacheOutdateChecker tariffCacheOutdateChecker)
    {
        this.tariffsAndRolesApiClient = tariffsAndRolesApiClient;
        this.tariffCacheOutdateChecker = tariffCacheOutdateChecker;
    }

    public void Invalidate()
    {
        cacheTariffsAndRoles = null;
    }

    public async Task<TariffsAndRoles> GetTariffsAndRolesAsync()
    {
        if (await tariffCacheOutdateChecker.IsOutdatedAsync().ConfigureAwait(false))
        {
            cacheTariffsAndRoles = null;
        }

        var сacheTariffsAndRolesRef = cacheTariffsAndRoles;

        if (сacheTariffsAndRolesRef != null)
        {
            // иногда мы вернём старое значение, но никогда null
            return сacheTariffsAndRolesRef;
        }

        cacheTariffsAndRoles = await CreateRulesMapsAsync().ConfigureAwait(false);
        tariffCacheOutdateChecker.OnCacheRefreshed();
        return cacheTariffsAndRoles;
    }

    private async Task<TariffsAndRoles> CreateRulesMapsAsync()
    {
        var tariffsAndRolesDto = await tariffsAndRolesApiClient.GetAllAsync().ConfigureAwait(false);

        return new TariffsAndRoles
        {
            Tariffs = tariffsAndRolesDto.Tariffs.ToDictionary(t => t.Id, t => new TariffInfo
            {
                Id = t.Id,
                Name = t.Name,
                ProductGroup = t.ProductGroup,
                ProductPlatform = t.ProductPlatform,
                AccessRules = new HashSet<AccessRule>(t.AccessRules)
            }),
            Roles = tariffsAndRolesDto.RoleInfos.ToDictionary(r => r.Id, r => new RoleInfo
            {
                Id = r.Id,
                Name = r.Name,
                RoleCode = r.RoleCode,
                AccessRules = new HashSet<AccessRule>(r.AccessRules)
            })
        };
    }
}