namespace Moedelo.InfrastructureV2.Domain.Models.Setting;

public static class SettingValueExtensions
{
    public static SettingValue Required(this SettingValue value)
        => value.ThrowExceptionIfNull(true);
}
