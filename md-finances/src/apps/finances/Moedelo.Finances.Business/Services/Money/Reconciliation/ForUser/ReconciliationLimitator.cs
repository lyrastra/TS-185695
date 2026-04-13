using Moedelo.Finances.Domain.Interfaces.Business.Money.Reconciliation;
using Moedelo.Finances.Domain.Models.Money.Reconciliation;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Redis;
using System;
using System.Threading.Tasks;

namespace Moedelo.Finances.Business.Services.Money.Reconciliation.ForUser
{
    //Ограничения сверки:
    //1. Нельзя запраши Даннын по сверке за один и тотже переод несколько раз
    //2. Лимит внутренних циклов сверки
    [InjectAsSingleton]
    internal class ReconciliationLimitator : IReconciliationLimitator
    {
        private const int MaxCycleNumber = 500;
        private readonly IDefaultRedisDbExecuter redisDbExecuter;
        public ReconciliationLimitator(IDefaultRedisDbExecuter redisDbExecuter)
        {
            this.redisDbExecuter = redisDbExecuter;
        }

        public async Task<ReconciliationLimits> SetLimitsAsync(ReconciliationForUserRequest request)
        {
            var limits = await GetLimitsAsync(request).ConfigureAwait(false);
            limits.CurrentCycleNumber = limits.CurrentCycleNumber + 1;
            limits.ReconciledDate = request.StartDate;
            bool valueIsSet = await redisDbExecuter.SetValueForKeyAsync(ReconciliationRedisKeyGenerator.GetKey(request), limits).ConfigureAwait(false);
            if (!valueIsSet)
                throw new Exception($"Not set reconciliation limit. Reconciliation cycle: SessionId : {request.SessionId} StartDate : {request.StartDate} EndDate : {request.EndDate}");
            return limits;

        }

        public Task DeleteLimitsAsync(ReconciliationForUserRequest request)
        {
            return redisDbExecuter.DeleteKeyAsync(ReconciliationRedisKeyGenerator.GetKey(request));
        }

        public async Task<(bool?, ReconciliationLimits)> CheckLimitsAsync(ReconciliationForUserRequest request)
        {
            var limits = await GetLimitsAsync(request).ConfigureAwait(false);
            if (limits.CurrentCycleNumber > MaxCycleNumber)
                return (false, limits);

            if (request.StartDate >= limits.ReconciledDate)
                return (null, limits);

            return (true, limits);
        }

        public int GetMaxCycleNumber()
        {
            return MaxCycleNumber;
        }

        public async Task<ReconciliationLimits> GetLimitsAsync(ReconciliationForUserRequest request)
        {
            var key = ReconciliationRedisKeyGenerator.GetKey(request);
            var value = await redisDbExecuter.GetValueByKeyAsync<ReconciliationLimits>(key).ConfigureAwait(false);
            return value ?? new ReconciliationLimits();
        }

        private static class ReconciliationRedisKeyGenerator
        {
            public static string GetKey(ReconciliationForUserRequest request)
            {
                return $"Reconciliation:{request.SessionId}:Limits";
            }
        }
    }
}
