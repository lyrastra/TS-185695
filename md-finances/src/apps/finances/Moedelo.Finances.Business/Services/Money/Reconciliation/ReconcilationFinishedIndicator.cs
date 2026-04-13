using Moedelo.Finances.Domain.Interfaces.Business.Money.Reconciliation;
using Moedelo.Finances.Domain.Models.Money.Reconciliation;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Redis;
using System.Threading.Tasks;

namespace Moedelo.Finances.Business.Services.Money.Reconciliation
{
    [InjectAsSingleton]
    internal class ReconcilationFinishedIndicator : IReconcilationFinishedIndicator
    {
        private readonly IDefaultRedisDbExecuter redisDbExecuter;

        public ReconcilationFinishedIndicator(IDefaultRedisDbExecuter redisDbExecuter)
        {
            this.redisDbExecuter = redisDbExecuter;
        }
        public Task IdicatorOnAsync(int firmId, ReconcilationFinishedIndicatorData data)
        {
            return redisDbExecuter.SetValueForKeyAsync(Key(firmId), data);
        }

        public async Task<ReconcilationFinishedIndicatorData> LetSeeAsync(int firmId)
        {
            var idicator = await redisDbExecuter.GetValueByKeyAsync<ReconcilationFinishedIndicatorData>(Key(firmId)).ConfigureAwait(false);
            if (idicator != null)
            {
                await IdicatorOffAsync(firmId);
            }
            return idicator;
        }

        private Task IdicatorOffAsync(int firmId)
        {
            return redisDbExecuter.DeleteKeyAsync(Key(firmId));
        }

        private string Key(int firmId)
        {
            return $"MoneyReconcilation:FirmId:{firmId}:Finished";
        }
    }
}
