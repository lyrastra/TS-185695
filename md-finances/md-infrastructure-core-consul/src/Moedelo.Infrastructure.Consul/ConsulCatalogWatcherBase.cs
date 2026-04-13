using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Consul.Abstraction;
using Moedelo.Infrastructure.Consul.Extensions;
using Moedelo.Infrastructure.Consul.Internals;

namespace Moedelo.Infrastructure.Consul;

public abstract class ConsulCatalogWatcherBase : IConsulCatalogWatcher, IDisposable, IAsyncDisposable
{
    private const int WaitOnConsulSideTimeoutSec = 1;
    private const int MaximumInitialDataLoadingAttempts = 5;
    private static readonly KeyValuePair<string, string>[] EmptySettings = [];

    private bool disposed;
    private readonly CancellationTokenSource cancelOnDispose = new();
    private ConcurrentBag<WatchingTask> watchTasks = [];

    public event IConsulCatalogWatcher.ErrorHandler OnError;
    public event IConsulCatalogWatcher.RestoreAfterErrorHandler OnRestoreAfterError;

    #region Dispose

    public void Dispose()
    {
        Dispose(true);
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore().ConfigureAwait(false);

        Dispose(disposing: false);
    }

    private async ValueTask DisposeAsyncCore()
    {
        if (!cancelOnDispose.IsCancellationRequested)
        {
            cancelOnDispose.Cancel();

            // ошибки игнорируем, чтобы не получать лишних записей в системный Event Log
            await watchTasks.ToArray()
                .DisposeIgnoringExceptionsAsync()
                .ConfigureAwait(false);

            Interlocked.Exchange(ref watchTasks, []);
            cancelOnDispose.Dispose();
        }
    }

    private void Dispose(bool disposing)
    {
        if (disposed)
        {
            return;
        }

        if (disposing)
        {
            if (!cancelOnDispose.IsCancellationRequested)
            {
                cancelOnDispose.Cancel();
                // ошибки игнорируем, чтобы не получать лишних записей в системный Event Log
                watchTasks.ToArray().DisposeIgnoringExceptions();
                Interlocked.Exchange(ref watchTasks, []);
                cancelOnDispose.Dispose();
            }
        }

        disposed = true;
    }

    private void CheckDisposed()
    {
        if (disposed)
        {
            throw new ObjectDisposedException(nameof(ConsulCatalogWatcher));
        }
    }

    #endregion

    public void WatchKey(string keyPath, Func<string, Task> onChange, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(keyPath))
        {
            throw new ArgumentException("Не может быть пустым значением", nameof(keyPath));
        }

        Debug.Assert(onChange != null, nameof(onChange));

        CheckDisposed();

        watchTasks.Add(WatchingTask.StartLongRunningTask(keyPath,
            cancellation => WatchKeyTaskAsync(keyPath, onChange, cancellation),
            CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, cancelOnDispose.Token)));
    }

    public void WatchDirectory(
        string dirPath,
        Action<IReadOnlyCollection<KeyValuePair<string, string>>> onChange,
        CancellationToken cancellationToken)
    {
        WatchDirectory(dirPath, OnChangeAsync, cancellationToken);
        return;

        Task OnChangeAsync(IReadOnlyCollection<KeyValuePair<string, string>> map, CancellationToken _)
        {
            onChange(map);
            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// Установить наблюдение за изменениями каталога параметров в consul
    /// </summary>
    /// <param name="dirPath">путь до каталога</param>
    /// <param name="onChange">обработчик обнаруженных изменений</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    public void WatchDirectory(
        string dirPath,
        Func<IReadOnlyCollection<KeyValuePair<string, string>>, CancellationToken, Task> onChange,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(dirPath))
        {
            throw new ArgumentNullException(nameof(dirPath));
        }

        Debug.Assert(onChange != null, nameof(onChange));

        CheckDisposed();

        watchTasks.Add(WatchingTask.StartLongRunningTask( dirPath,
            cancellation => WatchDirectoryTaskAsync(dirPath, onChange, cancellation),
            CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, cancelOnDispose.Token)));
    }

    /// <summary>
    /// Цикл слежения за указанным ключом
    /// </summary>
    /// <param name="keyPath">путь до ключа в consul</param>
    /// <param name="onChange">обработчик, вызываемый при изменении ключа</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    private async Task WatchKeyTaskAsync(
        string keyPath,
        Func<string, Task> onChange,
        CancellationToken cancellationToken)
    {
        var path = keyPath.NormalizeConsulKeyPath();
        var queryParams = new ConsulQueryParams(recurse: false, WaitOnConsulSideTimeoutSec);

        var strategy = ConsulCatalogReloadingStrategy.GetBuilder(path)
            .WithWatchDelay(TimeSpan.FromSeconds(30))
            .WithDelayAfterNotFound(TimeSpan.FromSeconds(30))
            .WithDelaysAfterRunningErrors(ConsulCatalogReloadingStrategyDefaultSettings.WatchedCatalogReloadingDelays)
            .WithOnError(OnWatchError)
            .WithOnRestoreAfterError(OnWatchedCatalogRestoreAfterError)
            .BuildStrategy();

        while (cancellationToken.IsCancellationRequested == false)
        {
            try
            {
                var uriQuery = queryParams.GetQueryParamsString();
                using var response = await CallHttpGetConsulKvApiAsync(path, uriQuery, cancellationToken)
                    .ConfigureAwait(false);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    await strategy.DelayAfterNotFoundAsync(cancellationToken).ConfigureAwait(false);
                    continue;
                }

                if (!response.IsSuccessStatusCode)
                {
                    await strategy
                        .DelayAfterResponseWithNonSuccessStatusCodeAsync(queryParams, response.StatusCode, cancellationToken)
                        .ConfigureAwait(false);
                    continue;
                }

                var index = response.TryGetConsulIndex();

                if (queryParams.HasTheSameIndex(index))
                {
                    await strategy.DelayAfterNoChangesAsync(cancellationToken).ConfigureAwait(false);
                    continue;
                }

                // что-то изменилось - загружаем значение и вызываем обработчик
                var value = await response
                    .GetConsulValueKeyAsync(cancellationToken)
                    .ConfigureAwait(false);
                await onChange(value).ConfigureAwait(false);
                // запоминаем текущий индекс
                // индекс можно сохранять только после того, как обработчик вернёт управление (т.е. данные сохранятся)
                queryParams.Index = index;

                await strategy.DelayAfterChangesAppliedAsync(cancellationToken);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                break;
            }
            catch (HttpRequestException httpRequestException)
            {
                await strategy.DelayAfterErrorAsync(queryParams, httpRequestException, cancellationToken);
            }
        }
    }

    /// <summary>
    /// Цикл слежения за указанным каталогом ключей в consul
    /// </summary>
    /// <param name="directoryPath">путь до каталога ключей в consul</param>
    /// <param name="onChange">обработчик, вызываемый при изменении ключа</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    private async Task WatchDirectoryTaskAsync(
        string directoryPath,
        Func<IReadOnlyCollection<KeyValuePair<string, string>>, CancellationToken, Task> onChange,
        CancellationToken cancellationToken)
    {
        var path = directoryPath.NormalizeConsulKeyDirectoryPath();
        var queryParams = new ConsulQueryParams(recurse: true, WaitOnConsulSideTimeoutSec);
        var isFound = default(bool?);

        var strategy = ConsulCatalogReloadingStrategy.GetBuilder(path)
            .WithWatchDelay(TimeSpan.FromSeconds(30))
            .WithDelayAfterNotFound(TimeSpan.FromSeconds(30))
            .WithDelaysAfterRunningErrors(ConsulCatalogReloadingStrategyDefaultSettings.WatchedCatalogReloadingDelays)
            .WithOnError(OnWatchError)
            .WithOnRestoreAfterError(OnWatchedCatalogRestoreAfterError)
            .BuildStrategy();

        while (cancellationToken.IsCancellationRequested == false)
        {
            try
            {
                var uriQueryString = queryParams.GetQueryParamsString();
                using var response = await CallHttpGetConsulKvApiAsync(path, uriQueryString, cancellationToken)
                    .ConfigureAwait(false);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    if (isFound != false)
                    {
                        await onChange(EmptySettings, cancellationToken).ConfigureAwait(false);
                        isFound = false;
                    }

                    await strategy.DelayAfterNotFoundAsync(cancellationToken).ConfigureAwait(false);
                    continue;
                }

                if (!response.IsSuccessStatusCode)
                {
                    await strategy
                        .DelayAfterResponseWithNonSuccessStatusCodeAsync(queryParams, response.StatusCode, cancellationToken)
                        .ConfigureAwait(false);
                    continue;
                }

                isFound = true;
                var index = response.TryGetConsulIndex();

                if (queryParams.HasTheSameIndex(index))
                {
                    await strategy.DelayAfterNoChangesAsync(cancellationToken).ConfigureAwait(false);
                    continue;
                }

                // что-то изменилось - загружаем значение и вызываем обработчик
                var dictionary = await response
                    .BuildConsulKeyValueCollectionAsync(path)
                    .ConfigureAwait(false);
                await onChange(dictionary, cancellationToken).ConfigureAwait(false);

                // запоминаем текущий индекс
                // индекс можно сохранять только после того, как обработчик вернёт управление (т.е. данные сохранятся)
                queryParams.Index = index;

                await strategy.DelayAfterChangesAppliedAsync(cancellationToken);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                break;
            }
            catch (HttpRequestException httpRequestException)
            {
                await strategy.DelayAfterErrorAsync(queryParams, httpRequestException, cancellationToken);
            }
        }
    }

    private void OnWatchedCatalogRestoreAfterError(ConsulRestoreAfterErrorHandlerArgs args)
    {
        OnRestoreAfterError?.Invoke(args.RunningErrorsCount, args.KeyPath);
    }

    private void OnWatchError(ConsulErrorHandlerArgs args)
    {
        if (args.QueryParams.IsSetAtLeastOnce == false && args.RunningErrorsCount > MaximumInitialDataLoadingAttempts)
        {
            throw new Exception(
                $"Не удалось загрузить данные из {args.KeyPath} на старте приложения: {args.RunningErrorsCount} попыток подряд провалено");
        }

        OnError?.Invoke(args.RunningErrorsCount, args.KeyPath, args.LastErrorMessage);
    }

    /// <summary>
    /// Получить значение ключа по http протоколу
    /// </summary>
    /// <param name="keyPath">путь до ключа</param>
    /// <param name="uriQuery">query часть uri запроса</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    /// <returns>http ответ запроса</returns>
    protected abstract Task<HttpResponseMessage> CallHttpGetConsulKvApiAsync(
        string keyPath,
        string uriQuery,
        CancellationToken cancellationToken,
        // ReSharper disable InvalidXmlDocComment
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);
    // ReSharper restore InvalidXmlDocComment
}
