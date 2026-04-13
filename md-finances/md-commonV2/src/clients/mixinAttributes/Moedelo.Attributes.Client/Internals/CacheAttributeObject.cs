using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Helpers;
using Moedelo.InfrastructureV2.Domain.Interfaces.Redis;
using Moedelo.InfrastructureV2.Domain.Models.Cache;

namespace Moedelo.Attributes.Client.Internals
{
    public class CacheAttributeObject : CachedValue<IReadOnlyDictionary<string, AttributeObject>>
    {
        public CacheAttributeObject(Func<Task<IReadOnlyDictionary<string, AttributeObject>>> func,
            string key,
            IRedisDbExecuter redisDbExecuter,
            TimeSpan defaultCacheTimeOut,
            TimeSpan? versionCacheTimeOut)
            : base(func, defaultCacheTimeOut, CacheVersionManagerFactory.GetRedisCacheVersionManager(key, redisDbExecuter, versionCacheTimeOut))
        {
        }
    }
}