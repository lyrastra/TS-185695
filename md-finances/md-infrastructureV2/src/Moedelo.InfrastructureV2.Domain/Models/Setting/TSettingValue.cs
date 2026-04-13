using System;
using Moedelo.InfrastructureV2.Json;

namespace Moedelo.InfrastructureV2.Domain.Models.Setting;

public class TSettingValue<TValue>
{
    private readonly SettingValue settingValue;
    private readonly TValue defaultValue;
    private string lastRawValue = null;
    private TValue lastValue = default;

    public TSettingValue(SettingValue settingValue)
    {
        this.settingValue = settingValue;
        this.defaultValue = default;
        this.lastValue = default;
    }

    public TSettingValue(SettingValue settingValue, TValue defaultValue)
    {
        this.settingValue = settingValue;
        this.defaultValue = defaultValue;
        this.lastValue = defaultValue;
    }

    public TSettingValue<TValue> Required()
    {
        settingValue.Required();
        return this;
    }
    
    public TValue Value => GetValueOrDefault()
                        ?? throw new Exception($"Unable cast value of {settingValue.Name}={settingValue.Value} to {typeof(TValue).Name}");

    private TValue GetValueOrDefault()
    {
        var value = settingValue.Value;

        if (lastRawValue != value)
        {
            lastRawValue = value;
            lastValue = string.IsNullOrEmpty(value)
                ? defaultValue
                : value.FromJsonString<TValue>();
        }

        return lastValue;
    }
}
