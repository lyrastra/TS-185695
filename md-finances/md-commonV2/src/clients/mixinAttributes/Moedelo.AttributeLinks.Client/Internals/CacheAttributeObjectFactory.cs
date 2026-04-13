using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Redis;

namespace Moedelo.AttributeLinks.Client.Internals
{
    [InjectAsSingleton]
    public class CacheAttributeObjectFactory : ICacheAttributeLinkFactory
    {
        private readonly IAttributeLinkClient client;
        private readonly IDefaultRedisDbExecuter redisDbExecuter;

        private readonly ConcurrentDictionary<byte, CacheAttributeLink> cache
            = new ConcurrentDictionary<byte, CacheAttributeLink>();

        public CacheAttributeObjectFactory(IDefaultRedisDbExecuter redisDbExecuter,
            IAttributeLinkClient client)
        {
            this.redisDbExecuter = redisDbExecuter;
            this.client = client;
        }

        public async Task<CacheAttributeLink> GetAsync(byte type)
        {
            CacheAttributeLink result;
            if (cache.TryGetValue(type, out result))
            {
                return result;
            }

            var cached = await client.IsCachedAsync(type).ConfigureAwait(false);
            if (cached)
            {
                return cache.GetOrAdd(type, Create);
            }

            return cache.GetOrAdd(type, (CacheAttributeLink) null);
        }

        private CacheAttributeLink Create(byte type)
        {
            return new CacheAttributeLink(() => InnerGetAsync(type), GetKey(type),
                redisDbExecuter, new TimeSpan(0, 0, 5), new TimeSpan(0, 0, 1));
        }

        private async Task<IReadOnlyDictionary<string, IReadOnlyList<AttributeLink>>> InnerGetAsync(byte type)
        {
            var objs = await client.GetListForCacheAsync(type).ConfigureAwait(false);
            return objs
                .GroupBy(o => o.Name)
                .ToDictionary(g => g.Key, g => (IReadOnlyList<AttributeLink>)g.ToList());
        }

        private string GetKey(byte type)
        {
            return AttributeLinksCacheIdHelper.GetAttributeLink(type);
        }
    }
}