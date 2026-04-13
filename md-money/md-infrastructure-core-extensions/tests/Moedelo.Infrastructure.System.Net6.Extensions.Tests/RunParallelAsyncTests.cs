using System;
using System.Collections.Concurrent;
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
public class RunParallelAsyncTests
{
    [Test]
    public async Task ThrowsArgumentNullException_IfNullRefToCollectionPassed()
    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        int[] collection = default;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

        var act = async () => await collection!.RunParallelAsync(_ => Task.CompletedTask); 

        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Test]
    public async Task DoesThrowNothing_IfEmptyCollectionPassed()
    {
        var collection = Array.Empty<int>();

        var act = async () => await collection.RunParallelAsync(_ => Task.CompletedTask);
        
        await act.Should().NotThrowAsync();
    }

    [Test]
    public async Task DoesNotCallTaskFactory_IfEmptyCollectionPassed()
    {
        var taskFactory = new Mock<Func<int, Task>>();
        var collection = Array.Empty<int>();

        await collection.RunParallelAsync(taskFactory.Object); 

        taskFactory.Verify(foo => foo(It.IsAny<int>()), Times.Never);
    }

    [Test]
    public async Task CallsTaskFactoryWithCollectionItem()
    {
        var taskFactory = new Mock<Func<int, Task>>();

        taskFactory.Setup(func => func(It.IsAny<int>()))
            .Returns(Task.CompletedTask);
        
        var collection = new int[] { new Random().Next() };

        await collection.RunParallelAsync(taskFactory.Object); 

        taskFactory.Verify(foo => foo(It.Is<int>(v => v == collection[0])), Times.Once);
    }

    [Test]
    public async Task AwaitsTasksCompletion()
    {
        var random = TestContext.CurrentContext.Random;
        var collection = new int[] { random.Next(), random.Next(), random.Next() };
        var copied = new ConcurrentBag<int>();
        
        async Task Foo(int v)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(random.Next(20)));
            copied.Add(v);
        }

        await collection.RunParallelAsync(Foo);

        copied.Should().BeEquivalentTo(collection);
    }

    [Test]
    public async Task RunsTasksForAllInCollectionItems()
    {
        // случайно заполненная коллекция
        var collection = Enumerable
            .Range(1, (int)(EnumerableAsyncExtensions.DefaultMaxDegreeOfParallelism * 2.5))
            .ToList();

        var maxMs = collection.Count * 20;
        var delays = collection
            .Select((_, index) => TimeSpan.FromMilliseconds(maxMs - index * 20))
            .ToArray();
        
        var processOrder = new ConcurrentQueue<int>();
        
        async Task Foo(int value)
        {
            // сохраняем в 
            processOrder.Enqueue(value);
            await Task.Delay(delays[collection.IndexOf(value)]);
        }

        await collection.RunParallelAsync(Foo);

        processOrder.Should().BeEquivalentTo(collection);
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

        Task Fail(int value)
        {
            throw new Exception("TestException" + value);
        }

        var act = async () => await collection!.RunParallelAsync(Fail);

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

        Task Fail(int value)
        {
            throw new Exception("TestException" + value);
        }

        var exception = Assert.ThrowsAsync<AggregateException>(
            async () => await collection!.RunParallelAsync(Fail));

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

        async Task FailAfterDelay(int value)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(10));
            throw new Exception("TestException" + value);
        }

        var act = async () => await collection!.RunParallelAsync(FailAfterDelay);

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

        async Task FailAfterDelay(int value)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(10));
            throw new Exception("TestException" + value);
        }

        var exception = Assert.ThrowsAsync<AggregateException>(
            async () => await collection!.RunParallelAsync(FailAfterDelay));

        exception!.InnerExceptions.Should().HaveCount(collection.Count);
        exception!.InnerExceptions.Select(e => e.Message).Should().BeEquivalentTo(
            collection.Select(value => "TestException" + value));
    }

    [Test(Description = "Не выполняет одновременно более указанного количества задач")]
    public async Task LimitsSimultaneouslyRunningTaskAccordingToSpecifiedLimit()
    {
        const int limit = 4;
        var tasks = Enumerable.Range(1, 10)
            .Select(_ => new TaskCompletionSource())
            .ToArray();

        var processed = new ConcurrentStack<int>();

        Task Foo(TaskCompletionSource taskSource, CancellationToken ct)
        {
            processed.Push(1);

            return taskSource.Task;
        }

        // запускаем в фоне
        var _ = tasks.RunParallelAsync(Foo, limit, CancellationToken.None);

        // Ждём, пока не запустится хотя бы limit задач
        await WaitUntilConditionAsync(() => processed.Count >= limit, TimeSpan.FromSeconds(2));

        processed.Should().HaveCount(limit);

        foreach (var task in tasks)
            task.TrySetResult();
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
        var _ = tasks.RunParallelAsync(Foo, limit, CancellationToken.None);

        // Ждём, пока не запустится хотя бы limit задач
        await WaitUntilConditionAsync(() => processed.Count >= limit, TimeSpan.FromSeconds(2));

        tasks.First().SetResult(1);

        // Ждём, пока не запустится следующая задача
        await WaitUntilConditionAsync(() => processed.Count >= limit + 1, TimeSpan.FromSeconds(2));

        processed.Should().HaveCount(limit + 1);

        foreach (var task in tasks)
            task.TrySetResult(1);
    }

    private static async ValueTask WaitUntilConditionAsync(Func<bool> condition, TimeSpan timeout, int delayMs = 50)
    {
        var end = DateTime.Now + timeout;
        while (!condition() && DateTime.Now < end)
        {
            await Task.Delay(delayMs);
        }
    }

    [Test(Description = "Работа с AsyncLocal (случай с Task)")]
    public async Task DoesNotBreakAsyncLocalContract_WithTask([Values(true, false)]bool configureAwait)
    {
        var asyncLocalValue = new AsyncLocalValue<int>();
        
        var numbers = Enumerable.Range(1, 10)
            .Select(value => new NumbersPair(value))
            .ToArray();
        var expected = numbers
            .Select(pair => pair with { N2 = pair.N1 })
            .ToArray();
            
        await numbers.RunParallelAsync(Do, maxDegreeOfParallelism: 4);

        numbers.Should().BeEquivalentTo(expected);

        async Task CopyLocalAsyncValueToN2Async(NumbersPair pair)
        {
            await Task.Delay(5).ConfigureAwait(configureAwait);
            pair.N2 = asyncLocalValue.Value;
        }

        async Task Do(NumbersPair pair)
        {
            await Task.Delay(5).ConfigureAwait(configureAwait);
            using (asyncLocalValue.SetValue(pair.N1))
            {
                await Task.Delay(5).ConfigureAwait(configureAwait);
                await CopyLocalAsyncValueToN2Async(pair);
            }
        }
    }

    [Test(Description = "Работа с AsyncLocal (случай с ValueTask)")]
    public async Task DoesNotBreakAsyncLocalContract_WithValueTask([Values(true, false)]bool configureAwait)
    {
        var asyncLocalValue = new AsyncLocalValue<int>();
        
        var numbers = Enumerable.Range(1, 10)
            .Select(value => new NumbersPair(value))
            .ToArray();
        var expected = numbers
            .Select(pair => pair with { N2 = pair.N1 })
            .ToArray();
            
        await numbers.RunParallelAsync(Do, maxDegreeOfParallelism: 4);

        numbers.Should().BeEquivalentTo(expected);

        async ValueTask CopyLocalAsyncValueToN2Async(NumbersPair pair)
        {
            await Task.Delay(5).ConfigureAwait(configureAwait);
            pair.N2 = asyncLocalValue.Value;
        }

        async ValueTask Do(NumbersPair pair)
        {
            await Task.Delay(5).ConfigureAwait(configureAwait);
            using (asyncLocalValue.SetValue(pair.N1))
            {
                await Task.Delay(5).ConfigureAwait(configureAwait);
                await CopyLocalAsyncValueToN2Async(pair);
                await Task.Delay(5).ConfigureAwait(configureAwait);
            }
        }
    }
}
