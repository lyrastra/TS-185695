using System;

namespace Moedelo.Common.Settings.Abstractions.Models;

public class IntSettingValue
{
    private readonly SettingValue settingValue;
    private readonly int? defaultValue;
    private string lastRawValue = null;
    private int? lastIntValue = null;
    private readonly object lockObject = new object();

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

    public string Name => settingValue.Name;

    public int Value => GetIntValueOrDefault()
                        ?? throw new Exception($"Unable cast value of {settingValue.Name}={lastRawValue} to integer. Raw: {settingValue.Value}, Default: {defaultValue}");

    private int? GetIntValueOrDefault()
    {
        var value = settingValue.Value;

        lock (lockObject)
        {
            if (lastRawValue != value)
            {
                lastIntValue = int.TryParse(value, out var newIntValue) ? newIntValue : null;
                lastRawValue = value;
            }

            return lastIntValue ?? defaultValue;
        }
    }
    
}