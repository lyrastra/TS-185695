using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Operations.TaxationSystemChangingSync;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Operations.TaxationSystemChangingSync
{
    [InjectAsSingleton(typeof(ITaxationSystemChangingSyncObjectManager))]
    class TaxationSystemChangingSyncObjectManager : ITaxationSystemChangingSyncObjectManager
    {
        private readonly ChangeTaxationSystemNotifier changeTaxationSystemNotifier;
        private readonly TaxationSystemChangingSyncRedisExecuter redisDbExecuter;

        public TaxationSystemChangingSyncObjectManager(
            ChangeTaxationSystemNotifier changeTaxationSystemNotifier,
            TaxationSystemChangingSyncRedisExecuter redisDbExecuter)
        {
            this.changeTaxationSystemNotifier = changeTaxationSystemNotifier;
            this.redisDbExecuter = redisDbExecuter;
        }

        public async Task ChangeStateAsync(Guid guid, long documentBaseId)
        {
            var key = $"{TaxationSystemChangingSyncRedisExecuter.TaxationSystemChangingSyncKey}:{guid:N}";

            await redisDbExecuter.DeleteKeyAsync($"{key}:{documentBaseId}");
            
            var keys = redisDbExecuter.GetKeyListByMatch($"{key}:*");
            
            if (keys != null && keys.Any())
            {
                return;
            }
            
            await changeTaxationSystemNotifier.NotifyAsync(guid);
        }
    }
}
