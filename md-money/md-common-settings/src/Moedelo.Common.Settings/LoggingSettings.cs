using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.Settings;

[InjectAsSingleton(typeof(ILoggingSettings))]
internal sealed class LoggingSettings : ILoggingSettings
{
    private const string AppSettingsFileName = "appsettings.json";
    private const LogLevel DefaultLogLevel = LogLevel.Information;
    private IConfiguration Configuration { get; }

    public LoggingSettings()
    {
        this.Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(AppSettingsFileName)
            .Build();
        
        LogDirectoryPath = Environment.GetEnvironmentVariable("MD_LOGS_DIRECTORY") ?? GetDefaultValue();
    }

    public LogLevel MinLogLevel => MinLogLevelSetting?.Value
                                   ?? GetLogLevelFromConfigurationOrNull("LogLevel")
                                   ?? DefaultLogLevel;

    public EnumSettingValue<LogLevel> MinLogLevelSetting { private get; set; }

    public string LogDirectoryPath { get; }

    private static string GetDefaultValue()
    {
        const string directoryName = "MDLogs";
        var rootDirName = Directory.GetDirectoryRoot(Assembly.GetExecutingAssembly().Location);

        return Path.Combine(rootDirName, directoryName);
    }

    private LogLevel? GetLogLevelFromConfigurationOrNull(string settingName)
    {
        var str = Configuration[settingName];

        if (string.IsNullOrEmpty(str))
        {
            return null;
        }

        return (LogLevel)Enum.Parse(typeof(LogLevel), str);
    }
}

