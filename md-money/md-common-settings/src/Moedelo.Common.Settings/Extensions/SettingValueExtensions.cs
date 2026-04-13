namespace Moedelo.Common.Settings.Extensions;

internal static class SettingValueExtensions
{
    internal static string AddAppNameToDatabaseConnectionString(this string settingValue, string appName)
    {
        if (string.IsNullOrWhiteSpace(appName) || string.IsNullOrWhiteSpace(settingValue))
        {
            return settingValue;
        }

        if (settingValue.StartsWith("Data Source="))
        {
            return $"{settingValue}; Application Name={appName}";
        }

        if ((settingValue.StartsWith("Server=") || settingValue.StartsWith("Host=")) 
            && settingValue.Contains("Database=")
           )
        {
            return settingValue.EndsWith(";")
                ? $"{settingValue} ApplicationName={appName};" 
                : $"{settingValue}; ApplicationName={appName};";
        }

        return settingValue;
    }
}
