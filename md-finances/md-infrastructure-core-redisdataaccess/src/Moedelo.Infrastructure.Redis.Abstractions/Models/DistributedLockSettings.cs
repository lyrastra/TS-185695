using System;

namespace Moedelo.Infrastructure.Redis.Abstractions.Models
{
    public class DistributedLockSettings
    {
        public DistributedLockSettings(int attemptCount, TimeSpan delay, TimeSpan expiry, bool configureAwaitValue) 
            : this(attemptCount, delay, expiry)
        {
            ConfigureAwaitValue = configureAwaitValue;
        }
        
        public DistributedLockSettings(int attemptCount, TimeSpan delay, TimeSpan expiry)
        {
            if (attemptCount <= 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(attemptCount)} cann`t be 0 or less");
            }
            
            AttemptCount = attemptCount;
            Delay = delay;
            Expiry = expiry;
            ConfigureAwaitValue = false;
        }

        /// <summary> Количество попыток записать ключ в Redis </summary>
        public int AttemptCount { get; }

        /// <summary> Время, через которое осуществлять повторные попытки записи ключа </summary>
        public TimeSpan Delay { get; }

        /// <summary> Как долго хранить ключ в Redis</summary>
        public TimeSpan Expiry { get; }
        
        /// <summary> Значение, которое будет передано в ConfigureAwait() в стеке вызовов </summary>
        public bool ConfigureAwaitValue { get; }

        public static DistributedLockSettings Default { get; } =
            new DistributedLockSettings(1, new TimeSpan(), new TimeSpan(0, 10, 0));
    }
}