using System;

namespace Moedelo.Infrastructure.Consul.Internals;

internal abstract class ConsulCatalogReloadingStrategySettings
{
    private protected ConsulCatalogReloadingStrategySettings(string keyPath)
    {
        KeyPath = keyPath;
    }

    internal string KeyPath { get; }
    /// <summary>
    /// Пауза, если ничего не найдено
    /// </summary>
    internal TimeSpan DelayAfterNotFound { get; private protected set; } = TimeSpan.FromMinutes(5);
    /// <summary>
    /// Обычная пауза для опроса существующего каталога
    /// </summary>
    internal TimeSpan GeneralDelayTimeSpan { get; private protected set; }= TimeSpan.FromSeconds(30);
    /// <summary>
    /// Список пауз для нескольких ошибок, произошедших подряд
    /// </summary>
    internal TimeSpan[] DelayAfterRunningErrors { get; private protected set; }
    internal Action<ConsulErrorHandlerArgs> OnError { get; private protected set; }
    internal Action<ConsulRestoreAfterErrorHandlerArgs> OnRestoreAfterError { get; private protected set; }
}
