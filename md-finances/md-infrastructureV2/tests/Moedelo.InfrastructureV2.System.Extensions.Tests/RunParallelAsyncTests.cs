using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moedelo.Infrastructure.System.Extensions;
using Moq;
using NUnit.Framework;

namespace Moedelo.InfrastructureV2.System.Extensions.Tests;

public class RunParallelAsyncTests
{
    [Test]
    public async Task ThrowsArgumentNullException_IfNullRefToCollectionPassed()
    {
        int[] collection = default!;

        var act = async () => await collection!.RunParallelAsync(_ => Task.CompletedTask); 

        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Test]
    public async Task DoesThrowNothing_IfEmptyCollectionPassed()
    {
        var collection = Array.Empty<int>();

        var act = async () => await collection!.RunParallelAsync(_ => Task.CompletedTask);
        
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
        var random = new Random();
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
            .Select((i, index) => TimeSpan.FromMilliseconds(maxMs - index * 20))
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
        var random = new Random();

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
        var random = new Random();

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
        var random = new Random();

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
    public void ThrowsAggregateExceptionWithAllExceptionsInInnerExceptions__IfTaskFactoryThrowsExceptionAsync()
    {
        var random = new Random();

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
}
