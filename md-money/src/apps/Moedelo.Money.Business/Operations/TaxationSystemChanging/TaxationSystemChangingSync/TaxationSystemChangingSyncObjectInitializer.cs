using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Infrastructure.System.Extensions;

namespace Moedelo.Money.Business.Operations.TaxationSystemChangingSync
{
    [InjectAsSingleton(typeof(TaxationSystemChangingSyncObjectInitializer))]
    class TaxationSystemChangingSyncObjectInitializer
    {
        private readonly TaxationSystemChangingSyncRedisExecuter redisDbExecuter;

        public TaxationSystemChangingSyncObjectInitializer(
            TaxationSystemChangingSyncRedisExecuter redisDbExecuter)
        {
            this.redisDbExecuter = redisDbExecuter;
        }

        public Task InitializeAsync(Guid guid, IReadOnlyCollection<long> documentBaseIds)
        {
            var key = $"{TaxationSystemChangingSyncRedisExecuter.TaxationSystemChangingSyncKey}:{guid:N}";

            return documentBaseIds.RunParallelAsync(
                (id) => redisDbExecuter.SetValueForKeyAsync($"{key}:{id}", "empty", TimeSpan.FromDays(21)), 
                4);
        }
    }
}
