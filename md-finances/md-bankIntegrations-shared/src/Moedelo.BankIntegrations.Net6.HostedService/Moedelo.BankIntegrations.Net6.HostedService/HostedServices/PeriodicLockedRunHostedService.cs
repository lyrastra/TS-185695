using Microsoft.Extensions.Logging;
using Moedelo.Common.AspNet.HostedServices;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Redis.Abstractions;
using Moedelo.Infrastructure.AspNetCore.HostedServices;
using Moedelo.Infrastructure.Redis.Abstractions.Models;

namespace Moedelo.BankIntegrations.Net6.HostedService.HostedServices
{
    /// <summary>
    /// Hosted сервис, который последовательно вызывает <see cref="LockedExecuteAsync"/> через паузу <see cref="PeriodicHostedService.Interval"/>
    /// Сервис блокирует другие вызовы <see cref="LockedExecuteAsync"/> через Redis
    /// Как использовать:
    /// - создать класс наследник <see cref="PeriodicLockedRunHostedService"/>
    /// - реализовать метод <see cref="LockedExecuteAsync"/> в классе наследнике
    /// - для изменения периода вызова метода <see cref="DoExecuteTaskAsync"/> следует переопределить свойство <see cref="PeriodicHostedService.Interval"/>
    /// </summary>
    public abstract class PeriodicLockedRunHostedService : MoedeloPeriodicHostedService
    {
        private readonly IMoedeloRedisDbExecutorBase redisExecutor;
        private string DistributedLockKeyName => $"{GetType().Name}:DistributedLockKey";
        private string LockKeyName => $"{GetType().Name}:RedisLockKey";

        protected PeriodicLockedRunHostedService(
            IAuditTracer auditTracer,
            ILogger logger,
            IMoedeloRedisDbExecutorBase redisExecutor)
            : base(auditTracer, logger) =>
            this.redisExecutor = redisExecutor;

        /// <summary>
        /// Задача, которую необходимо выполнять
        /// </summary>
        /// <param name="cancellationToken"></param>
        protected abstract Task LockedExecuteAsync(CancellationToken cancellationToken);

        protected override Task DoExecuteTaskAsync(CancellationToken cancellationToken)
        {
            return redisExecutor.DistributedLockRunAsync(
                DistributedLockKeyName,
                () => ProcessAsync(cancellationToken),
                new DistributedLockSettings(1, TimeSpan.Zero, TimeSpan.FromMinutes(5)));
        }

        private async Task ProcessAsync(CancellationToken cancellationToken)
        {
            var isAllowed = await IsStartAllowedAsync();
            if (!isAllowed)
                return;

            await redisExecutor.SetValueForKeyAsync(LockKeyName, Guid.NewGuid().ToString(), Interval);

            await LockedExecuteAsync(cancellationToken);
        }

        private async Task<bool> IsStartAllowedAsync()
        {
            var redisValue = await redisExecutor.GetValueByKeyAsync(LockKeyName);
            return string.IsNullOrEmpty(redisValue);
        }
    }
}