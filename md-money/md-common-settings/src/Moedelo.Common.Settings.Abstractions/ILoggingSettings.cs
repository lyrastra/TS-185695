using Microsoft.Extensions.Logging;
using Moedelo.Common.Settings.Abstractions.Models;

namespace Moedelo.Common.Settings.Abstractions;

/// <summary>
/// Настройки системы логирования
/// </summary>
public interface ILoggingSettings
{
    /// <summary>
    /// Минимальный уровень логирования
    /// </summary>
    LogLevel MinLogLevel { get; }
    /// <summary>
    /// Настройка для получения текущего минимального уровня логирования
    /// </summary>
    public EnumSettingValue<LogLevel> MinLogLevelSetting { set; }
    /// <summary>
    /// Полный путь до каталога, в который должны писаться файлы логов
    /// </summary>
    string LogDirectoryPath { get; }
}
