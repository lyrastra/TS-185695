using Moedelo.Infrastructure.Redis.Abstractions.Models;
using Moedelo.InfrastructureV2.Domain.Models.Redis;

namespace Moedelo.InfrastructureV2.RedisDataAccess.Extensions
{
    internal static class RedisQueryObjectExtensions
    {
        internal static DistributedLockSettings ToLockSettings(this RedisQueryObject settings)
        {
            if (settings == null)
            {
                return null;
            }

            return new DistributedLockSettings(
                settings.AttemptCount,
                settings.Delay,
                settings.Expiry,
                settings.ConfigureAwaitValue);
        }
    }
}
