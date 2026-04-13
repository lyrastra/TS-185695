using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Infrastructure.System.Extensions
{

    public static partial class EnumerableAsyncExtensions
    {
        #region for Task

        /// <summary>
        /// Запустить для каждого элемента коллекции task и дождаться выполнения
        /// Обеспечивает одновременное выполнение не более <see cref="maxDegreeOfParallelism"/> Task'ов
        /// ВНИМАНИЕ: несмотря на то, что Task'и создаются в порядке расположения элементов <see cref="collection"/>
        /// реальный порядок начала исполнения созданных Task'ов неопределён и может отличаться от порядка размещения элементов в коллекции <see cref="collection"/> 
        /// </summary>
        /// <param name="collection">входная коллекция</param>
        /// <param name="taskFactory">функтор создания Task'а из элемента коллекции</param>
        /// <param name="maxDegreeOfParallelism">максимальное количество Task'ов, которые могут исполняться параллельно</param>
        /// <typeparam name="TSource">тип элемента коллекции</typeparam>
        public static Task RunParallelAsync<TSource>(this IEnumerable<TSource> collection,
            Func<TSource, Task> taskFactory,
            int maxDegreeOfParallelism = DefaultMaxDegreeOfParallelism)
        {
            return collection.RunParallelAsync(
                (item, _) => taskFactory(item),
                maxDegreeOfParallelism,
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
            async ValueTask ValueTaskFactory(TSource item, CancellationToken token) => await taskFactory(item, token);

            return collection.RunParallelAsync(ValueTaskFactory, maxDegreeOfParallelism, cancellationToken);
        }

        #endregion

        #region for ValueTask

        /// <summary>
        /// Запустить для каждого элемента коллекции task и дождаться выполнения
        /// Обеспечивает одновременное выполнение не более <see cref="maxDegreeOfParallelism"/> Task'ов
        /// ВНИМАНИЕ: несмотря на то, что Task'и создаются в порядке расположения элементов <see cref="collection"/>
        /// реальный порядок начала исполнения созданных Task'ов неопределён и может отличаться от порядка размещения элементов в коллекции <see cref="collection"/> 
        /// </summary>
        /// <param name="collection">входная коллекция</param>
        /// <param name="taskFactory">функтор создания Task'а из элемента коллекции</param>
        /// <param name="maxDegreeOfParallelism">максимальное количество Task'ов, которые могут исполняться параллельно</param>
        /// <typeparam name="TSource">тип элемента коллекции</typeparam>
        public static Task RunParallelAsync<TSource>(this IEnumerable<TSource> collection,
            Func<TSource, ValueTask> taskFactory,
            int maxDegreeOfParallelism = DefaultMaxDegreeOfParallelism)
        {
            return collection.RunParallelAsync(
                (item, _) => taskFactory(item),
                maxDegreeOfParallelism,
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
        public static async Task RunParallelAsync<TSource>(this IEnumerable<TSource> collection,
            Func<TSource, CancellationToken, ValueTask> taskFactory,
            int maxDegreeOfParallelism,
            CancellationToken cancellationToken)
        {
            if (collection is IReadOnlyCollection<TSource> { Count: 0 })
            {
                return;
            }

            var exceptions = new ConcurrentBag<Exception>();

            var parallelOptions = new ParallelOptions
            {
                MaxDegreeOfParallelism = maxDegreeOfParallelism,
                CancellationToken = cancellationToken
            };

            await Parallel.ForEachAsync(
                collection.Select((item, index) => (item, index)),
                parallelOptions,
                async (tuple, token) =>
                {
                    try
                    {
                        await taskFactory(tuple.item, token).ConfigureAwait(false);
                    }
                    catch (Exception e)
                    {
                        exceptions.Add(e);
                    }
                }).ConfigureAwait(false);

            if (!exceptions.IsEmpty)
            {
                throw new AggregateException(exceptions.ToArray());
            }
        }

        #endregion
    }
}
