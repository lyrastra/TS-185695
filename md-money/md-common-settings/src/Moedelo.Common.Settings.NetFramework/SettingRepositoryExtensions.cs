using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;

namespace Moedelo.Common.Settings.NetFramework
{
    public static class SettingRepositoryExtensions
    {
        public static SettingValue GetCoreSettingValue(this ISettingRepository settingRepository, string name)
        {
            return settingRepository.Get(name).ToCoreSettingValue();
        }
    }
}
