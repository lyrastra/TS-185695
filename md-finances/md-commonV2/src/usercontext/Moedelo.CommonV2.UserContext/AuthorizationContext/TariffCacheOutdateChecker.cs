using System;
using System.Threading.Tasks;
using Moedelo.CommonV2.UserContext.Domain.AuthorizationContext;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Redis;

namespace Moedelo.CommonV2.UserContext.AuthorizationContext;

[InjectAsSingleton(typeof(ITariffCacheOutdateChecker))]
public class TariffCacheOutdateChecker : ITariffCacheOutdateChecker
{
    private static readonly TimeSpan checkTimeout = new TimeSpan(0, 0, 20);
    private static DateTime nextCheckTime = DateTime.Now;
    private readonly ITariffRoleRedisDbExecuter redis;
    private DateTime cacheCreationTime;


    public TariffCacheOutdateChecker(ITariffRoleRedisDbExecuter redis)
    {
        this.redis = redis;
    }

    public async Task<bool> IsOutdatedAsync()
    {
        if (!redis.IsAvailable())
        {
            return false;
        }
            
        if (nextCheckTime > DateTime.Now)
        {
            return false;
        }

        var lastChangeDateString = await redis.GetValueByKeyAsync("TariffRoleLastChange").ConfigureAwait(false);
        nextCheckTime = DateTime.Now.Add(checkTimeout);
        if (!DateTime.TryParse(lastChangeDateString, out var lastChangeDate))
        {
            return false;
        }

        return cacheCreationTime < lastChangeDate;
    }
        
    public void OnCacheRefreshed()
    {
        cacheCreationTime = DateTime.Now;
    }
}