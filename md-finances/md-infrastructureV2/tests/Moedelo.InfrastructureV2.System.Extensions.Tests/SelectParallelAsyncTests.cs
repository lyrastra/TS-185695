using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moedelo.Infrastructure.System.Extensions;
using Moq;
using NUnit.Framework;

namespace Moedelo.InfrastructureV2.System.Extensions.Tests;

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
        var collection = new int[] { new Random().Next(int.MaxValue / 2) };
        var actual = await collection.SelectParallelAsync(v => Task.FromResult(v + 1));
        var expected = collection.Select(v => v + 1).ToList();

        actual.Should().BeEquivalentTo(expected);
    }

    [Test]
    public async Task ReturnsCollectionInProperOrder()
    {
        var random = new Random();

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
        var random = new Random();

        // случайно заполненная коллекция
        var collection = Enumerable
            .Range(1, (int)(EnumerableAsyncExtensions.DefaultMaxDegreeOfParallelism * 2.5))
            .Select(_ => random.Next())
            .ToList();

        var maxMs = collection.Count * 10;

        var delays = collection
            .Select((i, index) => TimeSpan.FromMilliseconds(maxMs - index * 10))
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
        var random = new Random();

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
        var random = new Random();

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
        var random = new Random();

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
    public void ThrowsAggregateExceptionWithAllExceptionsInInnerExceptions__IfTaskFactoryThrowsExceptionAsync()
    {
        var random = new Random();

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
}
