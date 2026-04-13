using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Extensions;
using Moedelo.Common.Settings.Abstractions.Models;

namespace Moedelo.Common.AccessRules.Extensions
{
    internal static class SettingRepositoryExtensions
    {
        internal static SettingValue GetRedisCacheConnectionSetting(this ISettingRepository settingRepository)
        {
            return settingRepository.Get("RedisCacheConnection");
        }

        internal static IntSettingValue GetTariffsAndRolesRedisDbNumber(this ISettingRepository settingRepository)
        {
            const int defaultValue = 1;
            return settingRepository.GetInt("TariffsAndRolesRedisDbNumber", defaultValue);
        }
        
        internal static IntSettingValue GetAuthorizationRedisDbNumber(this ISettingRepository settingRepository)
        {
            const int defaultValue = 4;
            return settingRepository.GetInt("AuthorizationRedisDbNumber", defaultValue);
        }
    }
}