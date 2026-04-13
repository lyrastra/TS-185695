using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.InfrastructureV2.RedisDataAccess.Extensions
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
