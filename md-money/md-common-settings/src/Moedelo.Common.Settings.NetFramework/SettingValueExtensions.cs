using SettingValueV2 = Moedelo.InfrastructureV2.Domain.Models.Setting.SettingValue;
using SettingValue = Moedelo.Common.Settings.Abstractions.Models.SettingValue;

namespace Moedelo.Common.Settings.NetFramework
{
    public static class SettingValueExtensions
    {
        public static SettingValue ToCoreSettingValue(this SettingValueV2 settingValue)
        {
            return new(settingValue.Name, _ => settingValue.Value);
        }
    }
}
