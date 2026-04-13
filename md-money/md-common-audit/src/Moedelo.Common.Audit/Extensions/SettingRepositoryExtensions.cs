using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Extensions;
using Moedelo.Common.Settings.Abstractions.Models;

namespace Moedelo.Common.Audit.Extensions
{
    internal static class SettingRepositoryExtensions
    {
        internal static SettingValue GetRedisConnectionSetting(this ISettingRepository settingRepository)
        {
            return settingRepository.Get("RedisConnection");
        }

        internal static IntSettingValue GetAuditRedisDbNumber(this ISettingRepository settingRepository)
        {
            const int defaultValue = 15;
            return settingRepository.GetInt("AuditRedisDbNumber", defaultValue);
        }
        
        internal static bool IsAuditTrailEnabled(this ISettingRepository settingRepository)
        {
            return settingRepository.Get("AuditManagementEnabled").GetBoolValueOrDefault(false);
        }
        
        internal static SettingValue GetAuditAppName(this ISettingRepository settingRepository)
        {
            return settingRepository.Get("AuditAppName");
        }

        internal static SettingValue GetAuditDbConnectionString(this ISettingRepository settingRepository)
        {
            return settingRepository.Get("AuditTrailConnectionString");
        }
    }
}
