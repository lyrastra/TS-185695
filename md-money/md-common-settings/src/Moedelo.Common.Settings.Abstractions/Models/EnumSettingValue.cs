using System;

namespace Moedelo.Common.Settings.Abstractions.Models;

public class EnumSettingValue<TEnum> where TEnum : struct, Enum
{
    private readonly SettingValue settingValue;
    private readonly TEnum? defaultValue;
    private string lastStringValue = null;
    private TEnum? lastEnumValue = default;

    public EnumSettingValue(SettingValue settingValue)
    {
        this.settingValue = settingValue;
        this.defaultValue = null;
    }

    public EnumSettingValue(SettingValue settingValue, TEnum defaultValue)
    {
        this.settingValue = settingValue;
        this.defaultValue = defaultValue;
    }

    public TEnum Value => GetValueOrDefault()
                          ?? throw new Exception(
                              $"Unable cast value of {settingValue.Name}={settingValue.Value} to {typeof(TEnum).Name}");

    private TEnum? GetValueOrDefault()
    {
        var value = settingValue.Value;

        if (lastStringValue != value)
        {
            lastStringValue = value;
            lastEnumValue = Enum.TryParse<TEnum>(value, out var newValue) ? newValue : null;
        }

        return lastEnumValue ?? defaultValue;
    }
}
