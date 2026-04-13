using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Settings.Abstractions.Models;

namespace Moedelo.Common.Settings.Extensions;

internal static partial class LoggerExtensions
{
    [LoggerMessage(EventId = 1001,
        Level = LogLevel.Information,
        Message = "Загрузка данных с '{keyPath}' восстановлена. Предшествовало {runningErrorsCount} ошибок подряд")]
    internal static partial void LogRestoreAfterError(this ILogger logger, string keyPath, int runningErrorsCount);

    [LoggerMessage(EventId = 1002,
        Level = LogLevel.Error,
        Message = "Ошибка загрузки данных с '{keyPath}'. Неудачных попыток подряд: {runningErrorsCount}. Последняя ошибка: {lastErrorMessage}")]
    internal static partial void LogLoadingError(this ILogger logger, string keyPath, int runningErrorsCount, string lastErrorMessage);

    internal static void LogDomainSettingsAreLoaded(this ILogger logger, string keyPath,
        IReadOnlyDictionary<string, string> settings, SettingsConfig config)
    {
        var extraData = new
        {
            config.Domain,
            config.AppName,
            KeyPath = keyPath, 
            settings.Count,
            Settings = settings
        };

        using (logger.BeginScope(new KeyValuePair<string, object>("ExtraData", extraData)))
        {
            logger.LogInformation("Загружены настройки уровня домена");
        }
    }

    internal static void LogApplicationSettingsAreLoaded(this ILogger logger, string keyPath,
        IReadOnlyDictionary<string, string> settings, SettingsConfig config)
    {
        var extraData = new
        {
            config.Domain,
            config.AppName,
            KeyPath = keyPath, 
            settings.Count,
            Settings = settings
        };

        using (logger.BeginScope(new KeyValuePair<string, object>("ExtraData", extraData)))
        {
            logger.LogInformation("Загружены настройки уровня приложения");
        }
    }

    internal static void LogCommonSettingsAreLoaded(this ILogger logger, string keyPath,
        IReadOnlyDictionary<string, string> settings, SettingsConfig config)
    {
        var extraData = new
        {
            config.Domain,
            config.AppName,
            KeyPath = keyPath, 
            settings.Count
        };

        using (logger.BeginScope(new KeyValuePair<string, object>("ExtraData", extraData)))
        {
            logger.LogInformation("Загружены общие настройки");
        }
    }

    internal static void LogGlobalSettingsAreLoaded(this ILogger logger, string keyPath,
        IReadOnlyDictionary<string, string> settings, SettingsConfig config)
    {
        var extraData = new
        {
            config.Domain,
            config.AppName,
            KeyPath = keyPath, 
            settings.Count
        };

        using (logger.BeginScope(new KeyValuePair<string, object>("ExtraData", extraData)))
        {
            logger.LogInformation("Загружены глобальные настройки");
        }
    }
}
