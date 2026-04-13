using FluentAssertions;
using Moedelo.Infrastructure.Consul.Extensions;
using Moq;

namespace Moedelo.Infrastructure.Consul.Tests;

public class SelectParallelAsyncTests
{
    private const int MaxDegreeOfParallelism = EnumerableAsyncExtensions.DefaultMaxDegreeOfParallelism;
    
    [Test]
    public async Task ThrowsArgumentNullException_IfNullRefToCollectionPassed()
    {
        int[] collection = default!;

        var act = async () => await collection!.SelectParallelAsync(
            (item, _) => Task.FromResult(item),
            MaxDegreeOfParallelism,
            CancellationToken.None); 

        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Test]
    public async Task ReturnsEmptyList_IfEmptyCollectionPassed()
    {
        var collection = Array.Empty<int>();

        var actual = await collection.SelectParallelAsync(
            (item, _) => Task.FromResult(item),
            MaxDegreeOfParallelism,
            CancellationToken.None); 

        actual.Should().BeEmpty();
    }

    [Test]
    public async Task DoesNotCallTaskFactory_IfEmptyCollectionPassed()
    {
        var taskFactory = new Mock<Func<int, CancellationToken, Task<int>>>();
        var collection = Array.Empty<int>();

        await collection.SelectParallelAsync(taskFactory.Object,
            MaxDegreeOfParallelism,
            CancellationToken.None); 

        taskFactory.Verify(foo => foo(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task CallsTaskFactoryWithCollectionItem()
    {
        var taskFactory = new Mock<Func<int, CancellationToken, Task<int>>>();

        taskFactory.Setup(func => func(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(0);
        
        var collection = new int[] { new Random().Next() };

        await collection.SelectParallelAsync(taskFactory.Object,
            MaxDegreeOfParallelism,
            CancellationToken.None);

        taskFactory.Verify(foo => foo(
                It.Is<int>(v => v == collection[0]),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Test]
    public async Task ReturnsCollectionOfTaskResults()
    {
        var collection = new [] { new Random().Next(int.MaxValue / 2) };
        var actual = await collection.SelectParallelAsync((v, _) => Task.FromResult(v + 1),
            MaxDegreeOfParallelism,
            CancellationToken.None);

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

        var actual = await collection.SelectParallelAsync(
            (item, _) => Task.FromResult(item),
            MaxDegreeOfParallelism,
            CancellationToken.None);

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

        async Task<int> Foo(int value, CancellationToken ct)
        {
            var delay = delays[collection.IndexOf(value)];
            await Task.Delay(delay, ct);

            return value;
        }

        var actual = await collection.SelectParallelAsync(Foo, MaxDegreeOfParallelism, CancellationToken.None);

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

        Task<int> Fail(int value, CancellationToken _)
        {
            throw new Exception("TestException" + value);
        }

        var act = async () => await collection!.SelectParallelAsync(Fail, MaxDegreeOfParallelism, CancellationToken.None);

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

        Task<int> Fail(int value, CancellationToken _)
        {
            throw new Exception("TestException" + value);
        }

        var exception = Assert.ThrowsAsync<AggregateException>(
            () => collection.SelectParallelAsync(Fail, MaxDegreeOfParallelism, CancellationToken.None));

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

        async Task<int> FailAfterDelay(int value, CancellationToken ct)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(10), ct);
            throw new Exception("TestException" + value);
        }

        var act = () => collection.SelectParallelAsync(
            FailAfterDelay,
            MaxDegreeOfParallelism,
            CancellationToken.None);

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

        async Task<int> FailAfterDelay(int value
            , CancellationToken ct)        {
            await Task.Delay(TimeSpan.FromMilliseconds(10), ct);
            throw new Exception("TestException" + value);
        }

        var exception = Assert.ThrowsAsync<AggregateException>(
            async () => await collection!.SelectParallelAsync(FailAfterDelay,
                MaxDegreeOfParallelism,
                CancellationToken.None));

        exception!.InnerExceptions.Should().HaveCount(collection.Count);
        exception!.InnerExceptions.Select(e => e.Message).Should().BeEquivalentTo(
            collection.Select(value => "TestException" + value));
    }
}
