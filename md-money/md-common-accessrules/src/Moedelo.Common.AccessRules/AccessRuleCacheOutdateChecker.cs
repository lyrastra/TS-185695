using System;
using System.Threading.Tasks;

using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Infrastructure;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.AccessRules
{
    [InjectAsSingleton(typeof(IAccessRuleCacheOutdateChecker))]
    internal class AccessRuleCacheOutdateChecker : IAccessRuleCacheOutdateChecker
    {
        private static readonly TimeSpan checkTimeout = new TimeSpan(0, 0, 20);
        private readonly ITariffsAndRolesRedisDbExecutor redis;
        private static DateTime nextCheckTime = DateTime.Now;
        private DateTime cacheCreationTime;
        

        public AccessRuleCacheOutdateChecker(ITariffsAndRolesRedisDbExecutor redis)
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
}
