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
        /// Получить для каждого элемента коллекции результат выполнения task
        /// Обеспечивает одновременное выполнение не более <see cref="maxDegreeOfParallelism"/> Task'ов
        /// ВНИМАНИЕ: несмотря на то, что Task'и создаются в порядке расположения элементов <see cref="collection"/>
        /// реальный порядок начала исполнения созданных Task'ов неопределён и может отличаться от порядка размещения элементов в коллекции <see cref="collection"/>
        /// В то же время, элементы в выходной коллекции размещаются соответственно элементам <see cref="collection"/> 
        /// </summary>
        /// <param name="collection">входная коллекция</param>
        /// <param name="taskFactory">функтор создания Task'а получения результата из элемента коллекции</param>
        /// <param name="maxDegreeOfParallelism">максимальное количество Task'ов, которые могут исполняться параллельно</param>
        /// <param name="cancellationToken">токен отмены операции</param>
        /// <typeparam name="TSource">тип элемента входной коллекции</typeparam>
        /// <typeparam name="TResult">тип результата обработки элемента входной коллекции</typeparam>
        public static Task<List<TResult>> SelectParallelAsync<TResult, TSource>(
            this IEnumerable<TSource> collection,
            Func<TSource, CancellationToken, Task<TResult>> taskFactory,
            int maxDegreeOfParallelism,
            CancellationToken cancellationToken)
        {
            async ValueTask<TResult> ValueTaskFactory(TSource item, CancellationToken token) =>
                await taskFactory(item, token);

            return collection.SelectParallelAsync(ValueTaskFactory, maxDegreeOfParallelism, cancellationToken);
        }

        /// <summary>
        /// Получить для каждого элемента коллекции результат выполнения task
        /// Обеспечивает одновременное выполнение не более <see cref="maxDegreeOfParallelism"/> Task'ов
        /// ВНИМАНИЕ: несмотря на то, что Task'и создаются в порядке расположения элементов <see cref="collection"/>
        /// реальный порядок начала исполнения созданных Task'ов неопределён и может отличаться от порядка размещения элементов в коллекции <see cref="collection"/>
        /// В то же время, элементы в выходной коллекции размещаются соответственно элементам <see cref="collection"/> 
        /// </summary>
        /// <param name="collection">входная коллекция</param>
        /// <param name="taskFactory">функтор создания Task'а получения результата из элемента коллекции</param>
        /// <param name="maxDegreeOfParallelism">максимальное количество Task'ов, которые могут исполняться параллельно</param>
        /// <param name="cancellationToken">токен отмены операции</param>
        /// <typeparam name="TSource">тип элемента входной коллекции</typeparam>
        /// <typeparam name="TResult">тип результата обработки элемента входной коллекции</typeparam>
        public static Task<List<TResult>> SelectParallelAsync<TResult, TSource>(
            this IEnumerable<TSource> collection,
            Func<TSource, Task<TResult>> taskFactory,
            int maxDegreeOfParallelism = DefaultMaxDegreeOfParallelism)
        {
            return collection.SelectParallelAsync(
                (item, _) => taskFactory(item),
                maxDegreeOfParallelism,
                CancellationToken.None);
        }

        #endregion

        #region for ValueTask

        /// <summary>
        /// Получить для каждого элемента коллекции результат выполнения task
        /// Обеспечивает одновременное выполнение не более <see cref="maxDegreeOfParallelism"/> Task'ов
        /// ВНИМАНИЕ: несмотря на то, что Task'и создаются в порядке расположения элементов <see cref="collection"/>
        /// реальный порядок начала исполнения созданных Task'ов неопределён и может отличаться от порядка размещения элементов в коллекции <see cref="collection"/>
        /// В то же время, элементы в выходной коллекции размещаются соответственно элементам <see cref="collection"/> 
        /// </summary>
        /// <param name="collection">входная коллекция</param>
        /// <param name="taskFactory">функтор создания Task'а получения результата из элемента коллекции</param>
        /// <param name="maxDegreeOfParallelism">максимальное количество Task'ов, которые могут исполняться параллельно</param>
        /// <param name="cancellationToken">токен отмены операции</param>
        /// <typeparam name="TSource">тип элемента входной коллекции</typeparam>
        /// <typeparam name="TResult">тип результата обработки элемента входной коллекции</typeparam>
        public static async Task<List<TResult>> SelectParallelAsync<TResult, TSource>(
            this IEnumerable<TSource> collection,
            Func<TSource, CancellationToken, ValueTask<TResult>> taskFactory,
            int maxDegreeOfParallelism,
            CancellationToken cancellationToken)
        {
            if (collection is IReadOnlyCollection<TSource> { Count: 0 })
            {
                return new List<TResult>();
            }

            var results = new ConcurrentDictionary<int, TResult>();
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
                        results[tuple.index] = await taskFactory(tuple.item, token).ConfigureAwait(false);
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

            return results
                .OrderBy(pair => pair.Key)
                .Select(pair => pair.Value)
                .ToList();
        }

        /// <summary>
        /// Получить для каждого элемента коллекции результат выполнения task
        /// Обеспечивает одновременное выполнение не более <see cref="maxDegreeOfParallelism"/> Task'ов
        /// ВНИМАНИЕ: несмотря на то, что Task'и создаются в порядке расположения элементов <see cref="collection"/>
        /// реальный порядок начала исполнения созданных Task'ов неопределён и может отличаться от порядка размещения элементов в коллекции <see cref="collection"/>
        /// В то же время, элементы в выходной коллекции размещаются соответственно элементам <see cref="collection"/> 
        /// </summary>
        /// <param name="collection">входная коллекция</param>
        /// <param name="taskFactory">функтор создания Task'а получения результата из элемента коллекции</param>
        /// <param name="maxDegreeOfParallelism">максимальное количество Task'ов, которые могут исполняться параллельно</param>
        /// <param name="cancellationToken">токен отмены операции</param>
        /// <typeparam name="TSource">тип элемента входной коллекции</typeparam>
        /// <typeparam name="TResult">тип результата обработки элемента входной коллекции</typeparam>
        public static Task<List<TResult>> SelectParallelAsync<TResult, TSource>(
            this IEnumerable<TSource> collection,
            Func<TSource, ValueTask<TResult>> taskFactory,
            int maxDegreeOfParallelism = DefaultMaxDegreeOfParallelism)
        {
            return collection.SelectParallelAsync(
                (item, _) => taskFactory(item),
                maxDegreeOfParallelism,
                CancellationToken.None);
        }

        #endregion
    }
}
