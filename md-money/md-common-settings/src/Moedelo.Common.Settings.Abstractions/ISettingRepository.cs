using Moedelo.Common.Settings.Abstractions.Models;

namespace Moedelo.Common.Settings.Abstractions
{
    public interface ISettingRepository
    {
        SettingValue Get(string settingName);
    }
}
