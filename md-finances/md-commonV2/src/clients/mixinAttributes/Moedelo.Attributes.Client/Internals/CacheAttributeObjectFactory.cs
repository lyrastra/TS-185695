using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.mixinAttributes;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Redis;

namespace Moedelo.Attributes.Client.Internals
{
    [InjectAsSingleton]
    public class CacheAttributeObjectFactory : ICacheAttributeObjectFactory
    {
        private readonly IAttributeClient client;
        private readonly IDefaultRedisDbExecuter redisDbExecuter;

        private readonly ConcurrentDictionary<AttributeObjectType, CacheAttributeObject> cache
            = new ConcurrentDictionary<AttributeObjectType, CacheAttributeObject>();

        public CacheAttributeObjectFactory(IDefaultRedisDbExecuter redisDbExecuter, IAttributeClient client)
        {
            this.redisDbExecuter = redisDbExecuter;
            this.client = client;
        }

        public CacheAttributeObject Get(AttributeObjectType type)
        {
            return cache.GetOrAdd(type, Create);
        }

        private CacheAttributeObject Create(AttributeObjectType type)
        {
            return new CacheAttributeObject(() => GetAsync(type), GetKey(type),
                redisDbExecuter, new TimeSpan(0, 0, 5), new TimeSpan(0, 0, 1));
        }

        private async Task<IReadOnlyDictionary<string, AttributeObject>> GetAsync(AttributeObjectType type)
        {
            var objs = await client.GetListForCacheAsync(type).ConfigureAwait(false);
            return objs.ToDictionary(o => o.Name);
        }

        private string GetKey(byte type)
        {
            return AttributesCacheIdHelper.GetAttributeObject(type);
        }
    }
}