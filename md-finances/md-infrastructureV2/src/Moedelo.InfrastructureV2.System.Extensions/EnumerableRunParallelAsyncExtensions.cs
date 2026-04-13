using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Infrastructure.System.Extensions;

public static partial class EnumerableAsyncExtensions
{
    /// <summary>
    /// Запустить для каждого элемента коллекции task и дождаться выполнения
    /// Обеспечивает одновременное выполнение не более <see cref="maxDegreeOfParallelism"/> Task'ов
    /// ВНИМАНИЕ: несмотря на то, что Task'и создаются в порядке расположения элементов <see cref="collection"/>
    /// реальный порядок начала исполнения созданных Task'ов неопределён и может отличаться от порядка размещения элементов в коллекции <see cref="collection"/> 
    /// </summary>
    /// <param name="collection">входная коллекция</param>
    /// <param name="taskFactory">функтор создания Task'а из элемента коллекции</param>
    /// <param name="maxDegreeOfParallelism">максимальное количество Task'ов, которые могут исполняться параллельно</param>
    /// <param name="continueOnCapturedContext">признак того, что захваченный контекст должен быть сохранён - параметр для ConfigureAwait</param>
    /// <typeparam name="TSource">тип элемента коллекции</typeparam>
    public static Task RunParallelAsync<TSource>(
        this IEnumerable<TSource> collection,
        Func<TSource, Task> taskFactory,
        int maxDegreeOfParallelism = DefaultMaxDegreeOfParallelism,
        bool continueOnCapturedContext = false)
    {
        return collection.RunParallelAsync(
            (item, _) => taskFactory(item),
            maxDegreeOfParallelism,
            continueOnCapturedContext,
            CancellationToken.None); 
    }

    /// <summary>
    /// Запустить для каждого элемента коллекции task и дождаться выполнения
    /// Обеспечивает одновременное выполнение не более <see cref="maxDegreeOfParallelism"/> Task'ов
    /// ВНИМАНИЕ: несмотря на то, что Task'и создаются в порядке расположения элементов <see cref="collection"/>
    /// реальный порядок начала исполнения созданных Task'ов неопределён и может отличаться от порядка размещения элементов в коллекции <see cref="collection"/> 
    /// </summary>
    /// <param name="collection">входная коллекция</param>
    /// <param name="taskFactory">функтор создания Task'а из элемента коллекции</param>
    /// <param name="maxDegreeOfParallelism">максимальное количество Task'ов, которые могут исполняться параллельно</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    /// <typeparam name="TSource">тип элемента коллекции</typeparam>
    public static Task RunParallelAsync<TSource>(this IEnumerable<TSource> collection,
        Func<TSource, CancellationToken, Task> taskFactory,
        int maxDegreeOfParallelism,
        CancellationToken cancellationToken)
    {
        return RunParallelAsync(
            collection,
            taskFactory,
            maxDegreeOfParallelism,
            false,
            cancellationToken);
    }

    /// <summary>
    /// Запустить для каждого элемента коллекции task и дождаться выполнения
    /// Обеспечивает одновременное выполнение не более <see cref="maxDegreeOfParallelism"/> Task'ов
    /// ВНИМАНИЕ: несмотря на то, что Task'и создаются в порядке расположения элементов <see cref="collection"/>
    /// реальный порядок начала исполнения созданных Task'ов неопределён и может отличаться от порядка размещения элементов в коллекции <see cref="collection"/> 
    /// </summary>
    /// <param name="collection">входная коллекция</param>
    /// <param name="taskFactory">функтор создания Task'а из элемента коллекции</param>
    /// <param name="maxDegreeOfParallelism">максимальное количество Task'ов, которые могут исполняться параллельно</param>
    /// <param name="continueOnCapturedContext">признак того, что захваченный контекст должен быть сохранён - параметр для ConfigureAwait</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    /// <typeparam name="TSource">тип элемента коллекции</typeparam>
    public static async Task RunParallelAsync<TSource>(this IEnumerable<TSource> collection,
        Func<TSource, CancellationToken, Task> taskFactory,
        int maxDegreeOfParallelism,
        bool continueOnCapturedContext,
        CancellationToken cancellationToken)
    {
        if (collection is IReadOnlyCollection<TSource> { Count: 0 })
        {
            return;
        }

        var exceptions = new ConcurrentBag<Exception>();

        await Task
            .WhenAll(
            Partitioner.Create(collection).GetPartitions(maxDegreeOfParallelism)
                .Select(async partition =>
                {
                    using (partition)
                    {
                        while (!cancellationToken.IsCancellationRequested && partition.MoveNext())
                        {
                            try
                            {
                                await taskFactory(partition.Current, cancellationToken).ConfigureAwait(continueOnCapturedContext);
                            }
                            catch (Exception e)
                            {
                                exceptions.Add(e);
                            }
                        }
                    }
                }))
            .ConfigureAwait(continueOnCapturedContext);

        if (!exceptions.IsEmpty)
        {
            throw new AggregateException(exceptions.ToArray());
        }

        cancellationToken.ThrowIfCancellationRequested();
    }
}
