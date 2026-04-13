using Moedelo.Common.Settings.Abstractions.Models;

namespace Moedelo.Common.Settings.Abstractions.Extensions
{
    public static class SettingRepositoryExtensions
    {
        public static IntSettingValue GetInt(
            this ISettingRepository settingRepository,
            string settingName)
        {
            return new IntSettingValue(settingRepository.Get(settingName));
        }
        
        public static IntSettingValue GetInt(
            this ISettingRepository settingRepository,
            string settingName,
            int defaultValue)
        {
            return new IntSettingValue(settingRepository.Get(settingName), defaultValue);
        }

        public static EnumSettingValue<TEnum> GetEnum<TEnum>(
            this ISettingRepository settingRepository,
            string settingName,
            TEnum defaultValue) where TEnum : struct, System.Enum
        {
            return new EnumSettingValue<TEnum>(settingRepository.Get(settingName), defaultValue);
        }

        public static SettingValue Get(
            this ISettingRepository settingRepository,
            string settingName,
            string defaultValue)
        {
            var settingValue = settingRepository.Get(settingName);

            return new SettingValue(settingName, _ => settingValue.Value ?? defaultValue);
        }
    }
}
