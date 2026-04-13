using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;

namespace Moedelo.Money.Business.PaymentOrders.Import
{
    internal static class SettingRepositoryExtensions
    {
        internal static SettingValue GetRedisConnectionSetting(this ISettingRepository settingRepository)
        {
            return settingRepository.Get("RedisConnection");
        }

        internal static SettingValue GetRedisProductionMode(this ISettingRepository settingRepository)
        {
            return settingRepository.Get("RedisProductionMode");
        }

        internal static SettingValue GetPaymentOrderImportRedisDbNumber(this ISettingRepository settingRepository)
        {
            return SettingValue.CreateConstSettingValue("6");
        }
    }
}
