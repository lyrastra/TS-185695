using System;

namespace Moedelo.InfrastructureV2.Domain.Models.Setting;

public class IntSettingValue
{
    private readonly SettingValue settingValue;
    private readonly int? defaultValue;
    private string lastRawValue = null;
    private int? lastIntValue = null;

    public IntSettingValue(SettingValue settingValue)
    {
        this.settingValue = settingValue;
        this.defaultValue = null;
    }

    public IntSettingValue(SettingValue settingValue, int defaultValue)
    {
        this.settingValue = settingValue;
        this.defaultValue = defaultValue;
    }

    public IntSettingValue Required()
    {
        settingValue.Required();
        return this;
    }

    public int Value => GetIntValueOrDefault()
                        ?? throw new Exception($"Unable cast value of {settingValue.Name}={settingValue.Value} to integer");

    private int? GetIntValueOrDefault()
    {
        var value = settingValue.Value;

        if (lastRawValue != value)
        {
            lastRawValue = value;
            lastIntValue = int.TryParse(value, out var newIntValue) ? newIntValue : null;
        }

        return lastIntValue ?? defaultValue;
    }
}
