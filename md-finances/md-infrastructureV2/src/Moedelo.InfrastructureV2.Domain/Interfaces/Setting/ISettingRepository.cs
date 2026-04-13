using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.InfrastructureV2.Domain.Interfaces.Setting;

public interface ISettingRepository
{
    SettingValue Get(string settingName);
}