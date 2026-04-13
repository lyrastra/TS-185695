using FluentAssertions;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.ExecutionContext.Abstractions.Models;
using Moedelo.Common.Types;

namespace Moedelo.Common.ExecutionContext.Tests;

[TestFixture]
public class ExecutionInfoContextAccessorTests
{
    private ExecutionInfoContextAccessor accessor;

    private static string GenerateRandomToken() => TestContext.CurrentContext.Random.GetString(outputLength: 16);

    private static ExecutionInfoContext GenerateRandomContext() =>
        new ExecutionInfoContext
        {
            FirmId = new FirmId(TestContext.CurrentContext.Random.Next()),
            UserId = new UserId(TestContext.CurrentContext.Random.Next()),
            RoleId = new RoleId(TestContext.CurrentContext.Random.Next(maxValue: 10)),
            Scopes = Array.Empty<string>(),
            UserRules = Array.Empty<AccessRule>()
        };

    #region Поведение по умолчанию

    [SetUp]
    public void Setup()
    {
        accessor = new ExecutionInfoContextAccessor();
    }

    [Test(Description = "По умолчанию токен равен null")]
    public void ByDefault_TokenIsNull()
    {
        accessor.ExecutionInfoContextToken.Should().BeNull();
    }

    [Test(Description = "По умолчанию контекст равен null")]
    public void ByDefault_ContextIsNull()
    {
        accessor.ExecutionInfoContext.Should().BeNull();
    }

    #endregion

    #region Простые области

    [Test(Description = "Возвращает установленный токен внутри области действия")]
    public void SetContext_ChangesTokenUntilDisposeScope()
    {
        var token = GenerateRandomToken();
        var context = GenerateRandomContext();

        using var scope = accessor.SetContext(token, context);

        accessor.ExecutionInfoContextToken.Should().Be(token);
    }

    [Test(Description = "Возвращает токен null по выходу из корневой области действия")]
    public void SetContext_RestoreNullTokenAfterRoosScopeDispose()
    {
        var token = GenerateRandomToken();
        var context = GenerateRandomContext();

        using (var scope = accessor.SetContext(token, context))
        {
        }

        accessor.ExecutionInfoContextToken.Should().BeNull();
    }

    [Test(Description = "Возвращает установленный контекст внутри области действия")]
    public void SetContext_ChangesContextUntilDisposeScope()
    {
        var token = GenerateRandomToken();
        var context = GenerateRandomContext();

        using var scope = accessor.SetContext(token, context);

        accessor.ExecutionInfoContext.Should().Be(context);
    }
    
    
    [Test(Description = "Возвращает контекст null по выходу из корневой области действия")]
    public void SetContext_RestoreNullContextAfterRoosScopeDispose()
    {
        var token = GenerateRandomToken();
        var context = GenerateRandomContext();

        using (var scope = accessor.SetContext(token, context))
        {
        }

        accessor.ExecutionInfoContext.Should().BeNull();
    }

    #endregion

    #region Вложенные области

    [Test(Description = "Возвращает установленный токен внутри вложенной области действия")]
    public void SetContext_ChangesTokenUntilDisposeScopeInsideNestedScope()
    {
        var token = GenerateRandomToken();
        var context = GenerateRandomContext();

        using var scope = accessor.SetContext(token, context);

        var token2 = GenerateRandomToken();
        var context2 = GenerateRandomContext();

        using var scope2 = accessor.SetContext(token2, context2);

        accessor.ExecutionInfoContextToken.Should().Be(token2);
    }

    [Test(Description = "Возвращает установленный контекст внутри вложенной области действия")]
    public void SetContext_ChangesContextUntilDisposeScopeInsideNestedScope()
    {
        var token = GenerateRandomToken();
        var context = GenerateRandomContext();

        using var scope = accessor.SetContext(token, context);

        var token2 = GenerateRandomToken();
        var context2 = GenerateRandomContext();

        using var scope2 = accessor.SetContext(token2, context2);

        accessor.ExecutionInfoContext.Should().Be(context2);
    }

    [Test(Description = "Возвращает предыдущий установленный контекст по выходу из области действия")]
    public void SetContext_RestoreContextAfterDisposeScope()
    {
        var token = GenerateRandomToken();
        var context = GenerateRandomContext();

        using var scope = accessor.SetContext(token, context);

        var token2 = GenerateRandomToken();
        var context2 = GenerateRandomContext();

        using (var scope2 = accessor.SetContext(token2, context2))
        {
        }

        accessor.ExecutionInfoContext.Should().Be(context);
    }
    
    
    [Test(Description = "Возвращает предыдущий установленный токен по выходу из области действия")]
    public void SetContext_RestoreTokenAfterDisposeScope()
    {
        var token = GenerateRandomToken();
        var context = GenerateRandomContext();

        using var scope = accessor.SetContext(token, context);

        var token2 = GenerateRandomToken();
        var context2 = GenerateRandomContext();

        using (var scope2 = accessor.SetContext(token2, context2))
        {
        }

        accessor.ExecutionInfoContextToken.Should().Be(token);
    }

    #endregion

    #region Иные сценарии использования

    [Test(Description = "Сохраняется текущий, если был выставлен другой внутри async функции")]
    public async Task SetContext_KeepsCurrentValue_IfSetToAnotherInsideAsyncWrapper()
    {
        var token1 = GenerateRandomToken();
        var context1 = GenerateRandomContext();
        var token2 = GenerateRandomToken();
        var context2 = GenerateRandomContext();
        
        using var scope1 = accessor.SetContext(token1, context1);
        using var scope2 = await SetContextAsyncWrapper(token2, context2);

        accessor.ExecutionInfoContextToken.Should().Be(token1,
            because: "На этом уровне должен быть контекст, выставленный на этом уровне");

        return;

        async Task<IDisposable> SetContextAsyncWrapper(string? tokenValue, ExecutionInfoContext? contextValue)
        {
            await Task.Yield();
            return accessor.SetContext(tokenValue, contextValue);
        }
    }

    [Test(Description = "Разные значения в разных тредах")]
    public async Task SetContext_DiffValuesInDiffThreads()
    {
        var mutex = new Mutex(true);
        
        var token1 = GenerateRandomToken();
        var context1 = GenerateRandomContext();
        var token2 = GenerateRandomToken();
        var context2 = GenerateRandomContext();

        var queue = new List<string?>();
        var expected = new List<string?> { token1, token2 };

        async Task Worker(string tokenValue, ExecutionInfoContext contextValue)
        {
            await Task.Yield();
            using var _ = accessor.SetContext(tokenValue, contextValue);

            mutex.WaitOne();
            queue.Add(accessor.ExecutionInfoContextToken);
            mutex.ReleaseMutex();
        }

        var worker1 = Worker(token1, context1);
        var worker2 = Worker(token2, context2);
        mutex.ReleaseMutex();

        await Task.WhenAll(worker1, worker2);

        queue.Should().BeEquivalentTo(expected, options => options.WithoutStrictOrdering());
    }

    #endregion
}
