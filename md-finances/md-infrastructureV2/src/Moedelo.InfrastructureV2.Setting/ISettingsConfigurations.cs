using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.InfrastructureV2.Setting;

public interface ISettingsConfigurations
{
    SettingsConfig Config { get; }
}