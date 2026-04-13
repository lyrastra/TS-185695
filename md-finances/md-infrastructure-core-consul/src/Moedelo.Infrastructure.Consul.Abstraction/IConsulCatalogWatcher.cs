using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Infrastructure.Consul.Abstraction;

/// <summary>
/// Набор методов для слежения за изменениями ключей в Consul
/// </summary>
public interface IConsulCatalogWatcher
{
    /// <summary>
    /// Установить наблюдение за изменениями каталога параметров в consul
    /// </summary>
    /// <param name="dirPath">путь до каталога</param>
    /// <param name="onChange">обработчик обнаруженных изменений</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    void WatchDirectory(
        string dirPath,
        Action<IReadOnlyCollection<KeyValuePair<string, string>>> onChange,
        CancellationToken cancellationToken);

    /// <summary>
    /// Установить наблюдение за изменениями каталога параметров в consul
    /// </summary>
    /// <param name="dirPath">путь до каталога</param>
    /// <param name="onChange">обработчик обнаруженных изменений</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    void WatchDirectory(string dirPath,
        Func<IReadOnlyCollection<KeyValuePair<string, string>>, CancellationToken, Task> onChange,
        CancellationToken cancellationToken);

    /// <summary>
    /// Установить наблюдение за ключом в консул
    /// </summary>
    /// <param name="keyPath">путь до ключа</param>
    /// <param name="onChange">обработчик обнаруженных изменений</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    void WatchKey(
        string keyPath,
        Func<string, Task> onChange,
        CancellationToken cancellationToken);

    /// <summary>
    /// Обработчик оповещения об ошибке
    /// </summary>
    /// <param name="runningErrorsCount">количество ошибок подряд</param>
    /// <param name="keyPath">путь в consul, с которых идёт загрузка данных</param>
    /// <param name="lastErrorMessage">описание последней ошибки</param>
    delegate void ErrorHandler(int runningErrorsCount, string keyPath, string lastErrorMessage);

    /// <summary>
    /// Обработчик оповещения об успешной загрузке данных после ошибки
    /// </summary>
    /// <param name="runningErrorsCount">количество ошибок подряд</param>
    /// <param name="keyPath">путь в consul, с которых идёт загрузка данных</param>
    delegate void RestoreAfterErrorHandler(int runningErrorsCount, string keyPath);

    /// <summary>
    /// Оповещение "Произошла ошибка"
    /// </summary>
    event ErrorHandler OnError;

    /// <summary>
    /// Оповещение "Произошла успешная загрузка данных после ошибки"
    /// </summary>
    event RestoreAfterErrorHandler OnRestoreAfterError;
}