using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moedelo.Infrastructure.System.Extensions;
using Moedelo.Infrastructure.System.Net6.Extensions.Tests.Utils;
using Moq;
using NUnit.Framework;

namespace Moedelo.Infrastructure.System.Net6.Extensions.Tests;

[TestFixture]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[Parallelizable(ParallelScope.Children)]
[CancelAfter(5000)]
public class SelectParallelAsyncTests
{
    [Test]
    public async Task ThrowsArgumentNullException_IfNullRefToCollectionPassed()
    {
        int[] collection = default!;

        var act = async () => await collection!.SelectParallelAsync(Task.FromResult);

        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Test]
    public async Task ReturnsEmptyList_IfEmptyCollectionPassed()
    {
        var collection = Array.Empty<int>();

        var actual = await collection.SelectParallelAsync(Task.FromResult);

        actual.Should().BeEmpty();
    }

    [Test]
    public async Task DoesNotCallTaskFactory_IfEmptyCollectionPassed()
    {
        var taskFactory = new Mock<Func<int, Task<int>>>();
        var collection = Array.Empty<int>();

        await collection.SelectParallelAsync(taskFactory.Object);

        taskFactory.Verify(foo => foo(It.IsAny<int>()), Times.Never);
    }

    [Test]
    public async Task CallsTaskFactoryWithCollectionItem()
    {
        var taskFactory = new Mock<Func<int, Task<int>>>();

        taskFactory.Setup(func => func(It.IsAny<int>()))
            .ReturnsAsync(0);

        var collection = new int[] { new Random().Next() };

        await collection.SelectParallelAsync(taskFactory.Object);

        taskFactory.Verify(foo => foo(It.Is<int>(v => v == collection[0])), Times.Once);
    }

    [Test]
    public async Task ReturnsCollectionOfTaskResults()
    {
        var collection = new int[] { TestContext.CurrentContext.Random.Next(int.MaxValue / 2) };
        var actual = await collection.SelectParallelAsync(v => Task.FromResult(v + 1));
        var expected = collection.Select(v => v + 1).ToList();

        actual.Should().BeEquivalentTo(expected);
    }

    [Test]
    public async Task ReturnsCollectionInProperOrder()
    {
        var random = TestContext.CurrentContext.Random;

        // случайно заполненная коллекция
        var collection = Enumerable
            .Range(1, (int)(EnumerableAsyncExtensions.DefaultMaxDegreeOfParallelism * 2.5))
            .Select(_ => random.Next())
            .ToArray();

        var actual = await collection.SelectParallelAsync(Task.FromResult);

        actual.Should().BeEquivalentTo(collection, options => options.WithStrictOrdering());
    }

    [Test]
    public async Task ReturnsCollectionInProperOrder_IfProcessedWithRandomSpeed()
    {
        var random = TestContext.CurrentContext.Random;

        // случайно заполненная коллекция
        var collection = Enumerable
            .Range(1, (int)(EnumerableAsyncExtensions.DefaultMaxDegreeOfParallelism * 2.5))
            .Select(_ => random.Next())
            .ToList();

        var maxMs = collection.Count * 10;

        var delays = collection
            .Select((_, index) => TimeSpan.FromMilliseconds(maxMs - index * 10))
            .ToArray();

        async Task<int> Foo(int value)
        {
            var delay = delays[collection.IndexOf(value)];
            await Task.Delay(delay);

            return value;
        }

        var actual = await collection.SelectParallelAsync(Foo);

        actual.Should().BeEquivalentTo(collection, options => options.WithStrictOrdering());
    }

    [Test(Description = "Оборачивает исключения в AggregateException (кейс 1)")]
    public async Task ThrowsAggregateException_IfTaskFactoryThrowsExceptionSync()
    {
        var random = TestContext.CurrentContext.Random;

        // случайно заполненная коллекция
        var collection = Enumerable
            .Range(1, (int)(EnumerableAsyncExtensions.DefaultMaxDegreeOfParallelism * 2.5))
            .Select(_ => random.Next())
            .ToList();

        Task<int> Fail(int value)
        {
            throw new Exception("TestException" + value);
        }

        var act = async () => await collection!.SelectParallelAsync(Fail);

        await act.Should().ThrowExactlyAsync<AggregateException>();
    }

    [Test(Description = "Собирает исключения со всех тасков в AggregateException (кейс 1)")]
    public void ThrowsAggregateExceptionWithAllExceptionsInInnerExceptions_IfTaskFactoryThrowsExceptionSync()
    {
        var random = TestContext.CurrentContext.Random;

        // случайно заполненная коллекция
        var collection = Enumerable
            .Range(1, (int)(EnumerableAsyncExtensions.DefaultMaxDegreeOfParallelism * 2.5))
            .Select(_ => random.Next())
            .ToList();

        Task<int> Fail(int value)
        {
            throw new Exception("TestException" + value);
        }

        var exception = Assert.ThrowsAsync<AggregateException>(
            async () => await collection!.SelectParallelAsync(Fail));

        exception!.InnerExceptions.Should().HaveCount(collection.Count);
        exception!.InnerExceptions.Select(e => e.Message).Should().BeEquivalentTo(
            collection.Select(value => "TestException" + value));
    }

    [Test(Description = "Оборачивает исключения в AggregateException (кейс 2)")]
    public async Task ThrowsAggregateException_IfTaskFactoryThrowsExceptionAsync()
    {
        var random = TestContext.CurrentContext.Random;

        // случайно заполненная коллекция
        var collection = Enumerable
            .Range(1, (int)(EnumerableAsyncExtensions.DefaultMaxDegreeOfParallelism * 2.5))
            .Select(_ => random.Next())
            .ToList();

        async Task<int> FailAfterDelay(int value)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(10));
            throw new Exception("TestException" + value);
        }

        var act = async () => await collection!.SelectParallelAsync(FailAfterDelay);

        await act.Should().ThrowExactlyAsync<AggregateException>();
    }

    [Test(Description = "Собирает исключения со всех тасков в AggregateException (кейс 2)")]
    public void ThrowsAggregateExceptionWithAllExceptionsInInnerExceptions_IfTaskFactoryThrowsExceptionAsync()
    {
        var random = TestContext.CurrentContext.Random;

        // случайно заполненная коллекция
        var collection = Enumerable
            .Range(1, (int)(EnumerableAsyncExtensions.DefaultMaxDegreeOfParallelism * 2.5))
            .Select(_ => random.Next())
            .ToList();

        async Task<int> FailAfterDelay(int value)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(10));
            throw new Exception("TestException" + value);
        }

        var exception = Assert.ThrowsAsync<AggregateException>(
            async () => await collection!.SelectParallelAsync(FailAfterDelay));

        exception!.InnerExceptions.Should().HaveCount(collection.Count);
        exception!.InnerExceptions.Select(e => e.Message).Should().BeEquivalentTo(
            collection.Select(value => "TestException" + value));
    }

    [Test(Description = "Не выполняет одновременно более указанного количества задач")]
    public async Task LimitsSimultaneouslyRunningTaskAccordingToSpecifiedLimit()
    {
        const int limit = 4;
        var tasks = Enumerable.Range(1, 10)
            .Select(_ => new TaskCompletionSource<int>())
            .ToArray();

        var processed = new ConcurrentStack<int>();

        Task<int> Foo(TaskCompletionSource<int> taskSource, CancellationToken ct)
        {
            processed.Push(1);

            return taskSource.Task;
        }

        // запускаем в фоне
        var _ = tasks.SelectParallelAsync(Foo, limit, CancellationToken.None);

        // Ждём, пока не запустится хотя бы limit задач или не выйдет таймаут
        await WaitUntilConditionAsync(() => processed.Count >= limit, TimeSpan.FromSeconds(2));

        processed.Should().HaveCount(limit);

        foreach (var task in tasks)
            task.TrySetResult(1);
    }

    [Test(Description = "Стартует следующую задачу, если одна из максимального количества закончена")]
    public async Task StartsNextTaskUpToLimitAfterOneIsOver()
    {
        const int limit = 4;
        var tasks = Enumerable.Range(1, 10)
            .Select(_ => new TaskCompletionSource<int>())
            .ToArray();

        var processed = new ConcurrentStack<int>();

        Task<int> Foo(TaskCompletionSource<int> taskSource, CancellationToken ct)
        {
            processed.Push(1);

            return taskSource.Task;
        }

        // запускаем в фоне
        var _ = tasks.SelectParallelAsync(Foo, limit, CancellationToken.None);

        // Ждём, пока не запустится хотя бы limit задач
        await WaitUntilConditionAsync(() => processed.Count >= limit, TimeSpan.FromSeconds(2));

        tasks.First().SetResult(1);

        // Ждём, пока не запустится следующая задача
        await WaitUntilConditionAsync(() => processed.Count >= limit + 1, TimeSpan.FromSeconds(2));

        processed.Should().HaveCount(limit + 1);

        foreach (var task in tasks)
            task.TrySetResult(1);
    }


    [Test(Description = "Работа с AsyncLocal (случай с Task)")]
    public async Task DoesNotBreakAsyncLocalContract_WithTask([Values(true, false)] bool configureAwait)
    {
        var asyncLocalValue = new AsyncLocalValue<int>();

        var numbers = Enumerable.Range(1, 10)
            .Select(value => new NumbersPair(value))
            .ToArray();
        var expected = numbers
            .Select(pair => pair with { N2 = pair.N1 })
            .ToArray();

        var actual = await numbers.SelectParallelAsync(Do, maxDegreeOfParallelism: 4);

        actual.Should().BeEquivalentTo(expected);

        async Task<NumbersPair> CopyLocalAsyncValueToN2Async(NumbersPair pair)
        {
            await Task.Delay(5).ConfigureAwait(configureAwait);

            return pair with { N2 = asyncLocalValue.Value };
        }

        async Task<NumbersPair> Do(NumbersPair pair)
        {
            await Task.Delay(5).ConfigureAwait(configureAwait);
            using (asyncLocalValue.SetValue(pair.N1))
            {
                await Task.Delay(10).ConfigureAwait(configureAwait);

                return await CopyLocalAsyncValueToN2Async(pair).ConfigureAwait(configureAwait);
            }
        }
    }

    [Test(Description = "Работа с AsyncLocal (случай с ValueTask)")]
    public async Task DoesNotBreakAsyncLocalContract_WithValueTask([Values(true, false)] bool configureAwait)
    {
        var asyncLocalValue = new AsyncLocalValue<int>();

        var numbers = Enumerable.Range(1, 10)
            .Select(value => new NumbersPair(value))
            .ToArray();
        var expected = numbers
            .Select(pair => pair with { N2 = pair.N1 })
            .ToArray();

        var actual = await numbers.SelectParallelAsync(Do, maxDegreeOfParallelism: 4);

        actual.Should().BeEquivalentTo(expected);

        async ValueTask<NumbersPair> CopyLocalAsyncValueToN2Async(NumbersPair pair)
        {
            await Task.Delay(5).ConfigureAwait(configureAwait);

            return pair with { N2 = asyncLocalValue.Value };
        }

        async ValueTask<NumbersPair> Do(NumbersPair pair)
        {
            await Task.Delay(5).ConfigureAwait(configureAwait);
            using (asyncLocalValue.SetValue(pair.N1))
            {
                await Task.Delay(10).ConfigureAwait(configureAwait);

                return await CopyLocalAsyncValueToN2Async(pair);
            }
        }
    }
    
    private static async ValueTask WaitUntilConditionAsync(Func<bool> condition, TimeSpan timeout, int delayMs = 50)
    {
        var end = DateTime.Now + timeout;
        while (!condition() && DateTime.Now < end)
        {
            await Task.Delay(delayMs);
        }
    }
}
