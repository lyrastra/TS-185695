using FluentAssertions;
using Moedelo.Common.Audit.Logging;

namespace Moedelo.Common.Audit.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class SimpleListPoolTests
{
    [Test(Description = "Capture возвращает не null")]
    public void CaptureReturnsNotNull_UpToPoolCapacity()
    {
        const int poolCapacity = 2; 
        var listCapacity = TestContext.CurrentContext.Random.Next(5, 10); 
        var pool = new SimpleListPool<int>(poolCapacity, listCapacity);

        var list = pool.Capture();
        list.Should().NotBeNull();
    }

    [Test(Description = "Capture возвращает пустой список")]
    public void CaptureReturnsEmptyList()
    {
        const int poolCapacity = 2; 
        var listCapacity = TestContext.CurrentContext.Random.Next(5, 10); 
        var pool = new SimpleListPool<int>(poolCapacity, listCapacity);

        var list = pool.Capture();
        list.Count.Should().Be(0);
    }

    [Test(Description = "Capture возвращает список с ожидаемой вместимостью")]
    public void CaptureReturnsListWithDefinedCapacity()
    {
        const int poolCapacity = 2; 
        var listCapacity = TestContext.CurrentContext.Random.Next(5, 10); 
        var pool = new SimpleListPool<int>(poolCapacity, listCapacity);

        var list = pool.Capture();
        list.Capacity.Should().Be(listCapacity);
    }

    [Test(Description = "Capture возвращает не null даже если запрощено больше, чем вместимость пула")]
    public void CaptureReturnsNotNull_OverPoolCapacity()
    {
        const int poolCapacity = 2; 
        var listCapacity = TestContext.CurrentContext.Random.Next(5, 10); 
        var pool = new SimpleListPool<int>(poolCapacity, listCapacity);

        pool.Capture();
        pool.Capture();
        var list3 = pool.Capture();
        list3.Should().NotBeNull();
    }
    
    [Test(Description = "Capture возвращает список с ожидаемой вместимостью даже если он не из пула")]
    public void CaptureReturnsListWithDefinedCapacity_OverPoolCapacity()
    {
        const int poolCapacity = 2; 
        var listCapacity = TestContext.CurrentContext.Random.Next(5, 10); 
        var pool = new SimpleListPool<int>(poolCapacity, listCapacity);

        pool.Capture();
        pool.Capture();
        var list3 = pool.Capture();
        list3.Capacity.Should().Be(listCapacity);
    }
    
    [Test(Description = "при возврате в пул очищает список, взятый из пула")]
    public void ClearsList_OnReleasingToPool_IfListWasCapturedFromPool()
    {
        const int poolCapacity = 2; 
        var listCapacity = TestContext.CurrentContext.Random.Next(5, 10); 
        var pool = new SimpleListPool<int>(poolCapacity, listCapacity);

        var list = pool.Capture();
        list.Add(1);
        list.Add(2);
        pool.Release(list);

        list.Count.Should().Be(0);
    }

    [Test(Description = "при возврате в пул сохраняет вместимость списка, взятого из пула")]
    public void KeepsListCapacity_OnReleasingToPool_IfListWasCapturedFromPool()
    {
        const int poolCapacity = 2; 
        var listCapacity = TestContext.CurrentContext.Random.Next(5, 10); 
        var pool = new SimpleListPool<int>(poolCapacity, listCapacity);

        var list = pool.Capture();
        list.Add(1);
        list.Add(2);
        pool.Release(list);

        list.Capacity.Should().Be(listCapacity);
    }

    [Test(Description = "при возврате в пул очищает список, не взятый из пула")]
    public void ClearsList_OnReleasingToPool_IfListWasNotCapturedFromPool()
    {
        const int poolCapacity = 2; 
        var listCapacity = TestContext.CurrentContext.Random.Next(5, 10); 
        var pool = new SimpleListPool<int>(poolCapacity, listCapacity);

        var list = new List<int>();
        list.Add(1);
        list.Add(2);
        pool.Release(list);

        list.Count.Should().Be(0);
    }

    [Test(Description = "при возврате в пул сохраняет вместимость списка, не взятого из пула")]
    public void KeepsListCapacity_OnReleasingToPool_IfListWasNotCapturedFromPool()
    {
        const int poolCapacity = 2; 
        var listCapacity = TestContext.CurrentContext.Random.Next(5, 10); 
        var pool = new SimpleListPool<int>(poolCapacity, listCapacity);

        var strangeListCapacity = listCapacity + 3;
        var list = new List<int>(strangeListCapacity);
        list.Add(1);
        list.Add(2);
        pool.Release(list);

        list.Capacity.Should().Be(strangeListCapacity);
    }

    [Test(Description = "возвращает следующий свободный список из пула")]
    public void CaptureReturnsNextListFromPool()
    {
        const int poolCapacity = 2; 
        var listCapacity = TestContext.CurrentContext.Random.Next(5, 10); 
        var pool = new SimpleListPool<int>(poolCapacity, listCapacity);

        var list1 = pool.Capture();
        var list2 = pool.Capture();

        list2.Should().NotBeSameAs(list1);
    }

    [Test(Description = "возвращает свободный список из пула")]
    public void CaptureReturnsSingleReleased()
    {
        const int poolCapacity = 2; 
        var listCapacity = TestContext.CurrentContext.Random.Next(5, 10); 
        var pool = new SimpleListPool<int>(poolCapacity, listCapacity);

        var list1 = pool.Capture();
        var list2 = pool.Capture();
        pool.Release(list1);
        var list1Ref = pool.Capture();

        list1Ref.Should().BeSameAs(list1);
    }

    [Test(Description = "возвращает новый список, если в пуле не осталось свободных")]
    public void CaptureReturnsNewListIfNotOneInPool()
    {
        const int poolCapacity = 2; 
        var listCapacity = TestContext.CurrentContext.Random.Next(5, 10); 
        var pool = new SimpleListPool<int>(poolCapacity, listCapacity);

        var list1 = pool.Capture();
        var list2 = pool.Capture();
        var list3 = pool.Capture();

        list3.Should().NotBeSameAs(list1);
        list3.Should().NotBeSameAs(list2);
    }
}
