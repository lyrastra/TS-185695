using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;

namespace Moedelo.Common.Redis.Abstractions.Extensions
{
    internal static class SettingRepositoryExtensions
    {
        internal static SettingValue GetRedisKeyPrefixSetting(this ISettingRepository settingRepository)
        {
            return settingRepository.Get("RedisKeyPrefix");
        }
        
        internal static SettingValue GetRedisNeedToUseKeyPrefixSetting(this ISettingRepository settingRepository)
        {
            return settingRepository.Get("RedisNeedToUseKeyPrefix");
        }
    }
}
