using System;

namespace Moedelo.InfrastructureV2.Domain.Models.Setting;

public class SettingValue
{
    private readonly Func<string, string> getter;
    private bool throwExceptionIfNull;

    public string Name { get; }

    public SettingValue(string name, Func<string, string> getter)
    {
        this.getter = getter;
        this.throwExceptionIfNull = false;
        Name = name;
    }

    public SettingValue ThrowExceptionIfNull(bool value)
    {
        throwExceptionIfNull = value;
        return this;
    }

    public string Value
    {
        get
        {
            var value = getter.Invoke(Name);
            if (value == null && throwExceptionIfNull)
            {
                throw new ArgumentNullException(Name, $"Настройка {Name} или не задана или имеет значение null");
            }

            return value;
        }
    }

    public int GetIntValue()
    {
        return int.Parse(Value);
    }

    public int GetIntValueOrDefault(int defaultValue)
    {
        return int.TryParse(Value, out int result)
            ? result
            : defaultValue;
    }

    public DateTime GetDateTimeValueOrDefault(DateTime defaultValue)
    {
        return DateTime.TryParse(Value, out DateTime result)
            ? result
            : defaultValue;
    }
        
    public long GetLongValue()
    {
        return long.Parse(Value);
    }

    public long GetLongValueOrDefault(long defaultValue)
    {
        return long.TryParse(Value, out long result)
            ? result
            : defaultValue;
    }
        
    public bool GetBoolValue()
    {
        return bool.Parse(Value);
    }

    public bool GetBoolValueOrDefault(bool defaultValue)
    {
        return bool.TryParse(Value, out bool result)
            ? result
            : defaultValue;
    }

    public bool IsNull()
    {
        return Value == null;
    }

    [Obsolete("Используй IsNullOrEmpty")]
    public bool IsNullOrEmpry()
    {
        return string.IsNullOrEmpty(Value);
    }

    public bool IsNullOrEmpty()
    {
        return string.IsNullOrEmpty(Value);
    }

    public string GetStringValueOrDefault(string defaultValue)
    {
        return Value ?? defaultValue;
    }

    public static SettingValue CreateConstSettingValue(string value)
    {
        return new SettingValue(null, _ => value);
    }

    public override string ToString()
    {
        return Value;
    }
}