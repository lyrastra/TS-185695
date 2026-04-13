using System;

namespace Moedelo.Common.Settings.Abstractions.Models;

public class SettingValue
{
    private readonly Func<string, string> getter;
    private bool throwExceptionIfNull;

    public SettingValue(string name, Func<string, string> getter)
    {
        this.Name = name;
        this.getter = getter;
        this.throwExceptionIfNull = false;
    }

    public SettingValue ThrowExceptionIfNull(bool value)
    {
        throwExceptionIfNull = value;
        return this;
    }

    public string Name { get; }

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

    public static SettingValue CreateConstSettingValue(string value)
    {
        return new SettingValue(null, _ => value);
    }

    public static SettingValue CreateConstSettingValue(string name, string value)
    {
        return new SettingValue($"{name}:const", _ => value);
    }
}