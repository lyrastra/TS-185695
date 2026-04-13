using Microsoft.Extensions.Logging;
using Moedelo.Common.Consul.Abstractions;
using Moedelo.Infrastructure.Consul.Abstraction;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.Consul.AspNetCore;

[InjectAsSingleton(typeof(ConsulLoadingErrorLogger))]
internal sealed class ConsulLoadingErrorLogger : IDisposable
{
    private readonly IConsulCatalogWatcher consulCatalogWatcher;
    private readonly IMoedeloConsulCatalogWatcher moedeloConsulCatalogWatcher;
    private readonly ILogger logger;

    public ConsulLoadingErrorLogger(
        IConsulCatalogWatcher consulCatalogWatcher,
        ILogger<ConsulLoadingErrorLogger> logger,
        IMoedeloConsulCatalogWatcher moedeloConsulCatalogWatcher)
    {
        this.moedeloConsulCatalogWatcher = moedeloConsulCatalogWatcher;
        this.consulCatalogWatcher = consulCatalogWatcher;
        this.logger = logger;

        consulCatalogWatcher.OnError += OnConsulWatcherError;
        consulCatalogWatcher.OnRestoreAfterError += OnConsulWatcherRestoreAfterError;
        moedeloConsulCatalogWatcher.OnError += OnConsulWatcherError;
        moedeloConsulCatalogWatcher.OnRestoreAfterError += OnConsulWatcherRestoreAfterError;
    }

    public void Dispose()
    {
        moedeloConsulCatalogWatcher.OnError -= OnConsulWatcherError;
        moedeloConsulCatalogWatcher.OnRestoreAfterError -= OnConsulWatcherRestoreAfterError;
        consulCatalogWatcher.OnError -= OnConsulWatcherError;
        consulCatalogWatcher.OnRestoreAfterError -= OnConsulWatcherRestoreAfterError;
    }

    private void OnConsulWatcherRestoreAfterError(int runningErrorsCount, string keyPath)
    {
        logger.LogInformation("Загрузка данных с '{KeyPath}' восстановлена после {RunningErrorsCount} неудачных попыток подряд",
            keyPath, runningErrorsCount);
    }

    private void OnConsulWatcherError(int runningErrorsCount, string keyPath, string lastErrorMessage)
    {
        var needToLog = (1 < runningErrorsCount && runningErrorsCount <= 10) || runningErrorsCount % 10 == 0;

        if (needToLog)
        {
            logger.LogError("Ошибка загрузки данных с '{KeyPath}'. Неудачных попыток подряд: {RunningErrorsCount}. Последняя ошибка: {LastErrorMessage}",
                keyPath, runningErrorsCount, lastErrorMessage);
        }
    }
}