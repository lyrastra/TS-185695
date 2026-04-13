using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Infrastructure.Consul.Internals;

internal class ConsulCatalogReloadingStrategy
{
    internal static ConsulCatalogReloadingStrategyBuilder GetBuilder(string keyPath)
    {
        return new ConsulCatalogReloadingStrategyBuilder(keyPath);
    }

    /// <summary>
    /// Количество ошибок, произошедших подряд
    /// </summary>
    private int runningErrorsCount;
    /// <summary>
    /// Сообщение из последней ошибки
    /// </summary>
    private string lastErrorMessage;

    private readonly ConsulCatalogReloadingStrategySettings settings;

    internal ConsulCatalogReloadingStrategy(ConsulCatalogReloadingStrategySettings settings)
    {
        this.settings = settings;
        runningErrorsCount = 0;
        lastErrorMessage = null;
    }

    public Task DelayAfterNotFoundAsync(CancellationToken cancellationToken) =>
        ResetErrorsCountAndDelay(settings.DelayAfterNotFound, cancellationToken);

    public Task DelayAfterChangesAppliedAsync(CancellationToken cancellationToken) =>
        ResetErrorsCountAndDelay(settings.GeneralDelayTimeSpan, cancellationToken);

    public Task DelayAfterNoChangesAsync(CancellationToken cancellationToken) =>
        ResetErrorsCountAndDelay(settings.GeneralDelayTimeSpan, cancellationToken);

    public Task DelayAfterResponseWithNonSuccessStatusCodeAsync(ConsulQueryParams queryParams, HttpStatusCode statusCode, CancellationToken cancellationToken) =>
        IncrementErrorsCountAndDelayAsync(queryParams, $"Получен ответ с http-кодом {statusCode}", cancellationToken);

    public Task DelayAfterErrorAsync(ConsulQueryParams queryParams, Exception exception, CancellationToken cancellationToken) =>
        IncrementErrorsCountAndDelayAsync(queryParams, exception.Message, cancellationToken);

    private Task ResetErrorsCountAndDelay(TimeSpan timeSpan, CancellationToken cancellationToken)
    {
        if (runningErrorsCount > 0)
        {
            var args = new ConsulRestoreAfterErrorHandlerArgs(settings.KeyPath, runningErrorsCount);
            
            settings.OnRestoreAfterError?.Invoke(args);
        }

        runningErrorsCount = 0;
        lastErrorMessage = string.Empty;

        return Task.Delay(timeSpan, cancellationToken);
    }

    private Task IncrementErrorsCountAndDelayAsync(ConsulQueryParams queryParams, string errorMessage,
        CancellationToken cancellationToken)
    {
        runningErrorsCount += 1;
        lastErrorMessage = errorMessage;

        var delayIndex = Math.Min(settings.DelayAfterRunningErrors.Length - 1, runningErrorsCount);
        var timeSpan = settings.DelayAfterRunningErrors[delayIndex];
        var args = new ConsulErrorHandlerArgs(settings.KeyPath, queryParams, runningErrorsCount, lastErrorMessage);

        settings.OnError?.Invoke(args);

        return Task.Delay(timeSpan, cancellationToken);
    }
}