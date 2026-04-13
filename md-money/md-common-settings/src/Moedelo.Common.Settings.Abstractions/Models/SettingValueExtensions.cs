namespace Moedelo.Common.Settings.Abstractions.Models;

public static class SettingValueExtensions
{
    public static int GetIntValue(this SettingValue value)
    {
        return int.Parse(value.Value);
    }

    public static int GetIntValueOrDefault(this SettingValue value, int defaultValue)
    {
        return int.TryParse(value.Value, out var result)
            ? result
            : defaultValue;
    }
        
    public static long GetLongValue(this SettingValue value)
    {
        return long.Parse(value.Value);
    }

    public static long GetLongValueOrDefault(this SettingValue value, long defaultValue)
    {
        return long.TryParse(value.Value, out var result)
            ? result
            : defaultValue;
    }

    public static bool GetBoolValue(this SettingValue value)
    {
        return bool.Parse(value.Value);
    }

    public static bool GetBoolValueOrDefault(this SettingValue value, bool defaultValue)
    {
        return bool.TryParse(value.Value, out var result)
            ? result
            : defaultValue;
    }

    public static bool IsNull(this SettingValue value)
    {
        return value.Value == null;
    }

    public static bool IsNullOrEmpty(this SettingValue value)
    {
        return string.IsNullOrEmpty(value.Value);
    }

    public static string GetStringValueOrDefault(this SettingValue value, string defaultValue)
    {
        return value.Value ?? defaultValue;
    }
}
