using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Infrastructure.Consul.Abstraction;

public static class ConsulCatalogWatcherExtensions
{
    /// <summary>
    /// Установить наблюдение за изменениями каталога параметров в consul
    /// </summary>
    /// <param name="watcher"></param>
    /// <param name="dirPath">путь до каталога</param>
    /// <param name="onChange">обработчик обнаруженных изменений</param>
    /// <param name="cancellationToken">токен отмены ожидания первой загрузки</param>
    public static Task LoadAndWatchDirectoryAsync(
        this IConsulCatalogWatcher watcher,
        string dirPath,
        Action<IReadOnlyCollection<KeyValuePair<string, string>>> onChange,
        CancellationToken cancellationToken)
    {
        var completeOnce = new TaskCompletionSource<bool>();
        cancellationToken.Register(() => completeOnce.TrySetCanceled());

        watcher.WatchDirectory(
            dirPath,
            changes =>
            {
                onChange(changes);
                completeOnce.TrySetResult(true);
            },
            cancellationToken);

        return completeOnce.Task;
    }

    /// <summary>
    /// Установить наблюдение за изменениями каталога параметров в consul
    /// </summary>
    /// <param name="watcher"></param>
    /// <param name="dirPath">путь до каталога</param>
    /// <param name="onLoaded">обработчик обнаруженных изменений</param>
    /// <param name="cancellationToken">токен отмены ожидания первой загрузки</param>
    public static Task LoadDirectoryOnceAsync(
        this IConsulCatalogWatcher watcher,
        string dirPath,
        Action<IReadOnlyCollection<KeyValuePair<string, string>>> onLoaded,
        CancellationToken cancellationToken)
    {
        var watchCancellation = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        var completeOnce = new TaskCompletionSource<bool>();
        var registration = cancellationToken.Register(() =>
        {
            completeOnce.TrySetCanceled();
        });

        watcher.WatchDirectory(
            dirPath,
            changes =>
            {
                onLoaded(changes);

                if (!watchCancellation.IsCancellationRequested)
                {
                    watchCancellation.Cancel();
                }

                completeOnce.TrySetResult(true);
                watchCancellation.Dispose();
                registration.Dispose();
            },
            watchCancellation.Token);

        return completeOnce.Task;
    }
}
