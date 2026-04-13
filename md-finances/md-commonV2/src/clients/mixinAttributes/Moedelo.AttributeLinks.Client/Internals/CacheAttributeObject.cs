using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Helpers;
using Moedelo.InfrastructureV2.Domain.Interfaces.Redis;
using Moedelo.InfrastructureV2.Domain.Models.Cache;

namespace Moedelo.AttributeLinks.Client.Internals
{
    public class CacheAttributeLink : CachedValue<IReadOnlyDictionary<string, IReadOnlyList<AttributeLink>>>
    {
        public CacheAttributeLink(Func<Task<IReadOnlyDictionary<string, IReadOnlyList<AttributeLink>>>> func,
            string key,
            IDefaultRedisDbExecuter redisDbExecuter,
            TimeSpan defaultCacheTimeOut,
            TimeSpan? versionCacheTimeOut)
            : base(func, defaultCacheTimeOut, CacheVersionManagerFactory.GetRedisCacheVersionManager(key, redisDbExecuter, versionCacheTimeOut))
        {
        }
    }
}