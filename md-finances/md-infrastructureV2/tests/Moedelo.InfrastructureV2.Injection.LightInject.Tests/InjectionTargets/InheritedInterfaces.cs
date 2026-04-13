using System.Linq;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;

namespace Moedelo.InfrastructureV2.Injection.LightInject.Tests.InjectionTargets;

// Аналог IWebAppConfigCheck
public interface IBaseCheck
{
    void Check();
}

// Аналог IRedisCheck : IWebAppConfigCheck
public interface IDerivedCheck : IBaseCheck
{
}

// Аналог RedisCheck с атрибутом [InjectAsSingleton(typeof(IWebAppConfigCheck), typeof(IRedisCheck))]
[InjectAsSingleton(typeof(IBaseCheck), typeof(IDerivedCheck))]
public class TestCheck : IDerivedCheck
{
    public void Check()
    {
    }
}

// Класс для проверки резолвинга через IEnumerable<T> (аналог WebAppConfigChecker)
[InjectAsSingleton(typeof(ITestConfigChecker))]
public class TestConfigChecker : ITestConfigChecker
{
    private readonly IBaseCheck[] checks;

    public TestConfigChecker(System.Collections.Generic.IEnumerable<IBaseCheck> checks)
    {
        this.checks = checks.ToArray();
    }

    public int ChecksCount => checks.Length;
    public IBaseCheck[] Checks => checks;
}

public interface ITestConfigChecker
{
    int ChecksCount { get; }
    IBaseCheck[] Checks { get; }
}
