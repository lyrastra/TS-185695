using System;
using FluentAssertions;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.InfrastructureV2.Injection.Lightinject;
using Moedelo.InfrastructureV2.Injection.LightInject.Tests.InjectionTargets;
using Moq;
using NUnit.Framework;

namespace Moedelo.InfrastructureV2.Injection.LightInject.Tests;

[TestFixture]
[Parallelizable(ParallelScope.None)]
public class AppAutoDiInstallerTests
{
    private AppAutoDiInstaller diInstaller = null!;
    private readonly Mock<ILogger> loggerMock = new();

    [SetUp]
    public void SetUp()
    {
        diInstaller = new AppAutoDiInstaller(loggerMock.Object);
        diInstaller.Initialize();
    }

    [Test]
    public void InjectsSingletonByAttributeWithExplicitInterface_IsNotNull()
    {
        diInstaller.GetInstance<IExplicitSingleton>()
            .Should().NotBeNull();
    }

    [Test]
    public void InjectsSingletonByAttributeWithExplicitInterface_NotNull_ByImplementationType()
    {
        diInstaller.GetInstance<ExplicitSingleton>()
            .Should().NotBeNull();
    }

    [Test]
    public void InjectsSingletonByAttributeWithExplicitInterface_IsTypeOfImplementation()
    {
        diInstaller.GetInstance<IExplicitSingleton>().Should().BeOfType<ExplicitSingleton>();
    }

    [Test(Description = "Синглетон: возвращает один объект при разрешение по абстрактному и конкретному типам")]
    public void InjectsSingletonByAttributeWithExplicitInterface_IsSameAsByImplementationType()
    {
        diInstaller.GetInstance<IExplicitSingleton>().Should().Be(
            diInstaller.GetInstance<ExplicitSingleton>());
    }

    [Test]
    public void InjectsSingletonByAttributeWithExplicitInterface_IsTypeOfImplementation_ByImplementationType()
    {
        diInstaller.GetInstance<ExplicitSingleton>().Should().BeOfType<ExplicitSingleton>();
    }

    [Test]
    public void InjectsSingletonByAttributeWithDirectIdi_IsTypeOfImplementation()
    {
        diInstaller.GetInstance<DirectIdiSingleton>().Should().BeOfType<DirectIdiSingleton>();
    }

    [Test]
    public void InjectsSingletonByAttributeWithDirectImplicit_IsTypeOfImplementation()
    {
        diInstaller.GetInstance<DirectExplicitSingleton>().Should().BeOfType<DirectExplicitSingleton>();
    }

    [Test]
    public void InjectsSingletonByAttributeWithIdiInterface_NotNull()
    {
        diInstaller.GetInstance<IIDISingleton>().Should().NotBeNull();
    }

    [Test]
    public void InjectsSingletonByAttributeWithIdiInterface_NotNull_ByImplementationType()
    {
        diInstaller.GetInstance<IDISingleton>().Should().NotBeNull();
    }

    [Test]
    public void InjectsSingletonByAttributeWithIdiInterface_IsTypeOfImplementation()
    {
        diInstaller.GetInstance<IIDISingleton>().Should().BeOfType<IDISingleton>();
    }

    [Test]
    public void InjectsSingletonByAttributeWithIdiInterface_IsTypeOfImplementation_ByImplementationType()
    {
        diInstaller.GetInstance<IDISingleton>().Should().BeOfType<IDISingleton>();
    }

    [Test(Description = "Синглетон: явное указание нескольких интерфейсов в атрибуте")]
    public void InjectsSingletonByAttributeWithMultipleExplicit_NotNull_ByEachInterface()
    {
        diInstaller.GetInstance<IExplicitSingleton1>().Should().NotBeNull();
        diInstaller.GetInstance<IExplicitSingleton2>().Should().NotBeNull();
    }

    [Test]
    public void InjectsSingletonByAttributeWithMultipleExplicit_IsSameAsByAllAbstractType()
    {
        diInstaller.GetInstance<IExplicitSingleton1>().Should().Be(
            diInstaller.GetInstance<IExplicitSingleton2>());
    }

    [Test(Description = "Синглетон: возвращает один объект при разрешение по абстрактному и конкретному типам")]
    public void InjectsSingletonByAttributeWithMultipleExplicit_IsSameAsByImplementationType()
    {
        diInstaller.GetInstance<IExplicitSingleton1>().Should().Be(
            diInstaller.GetInstance<MultiExplicitSingleton>());
        diInstaller.GetInstance<IExplicitSingleton2>().Should().Be(
            diInstaller.GetInstance<MultiExplicitSingleton>());
    }

    [Test(Description = "Проверка того, что получение PerWebRequest сервиса вне скоупа вызывает исключение")]
    public void GetScopedServiceInstanceThrowsException_OutsideScope()
    {
        var action = () => diInstaller.GetInstance<IPerWebRequestClass>();

        action.Should().Throw<InvalidOperationException>();
    }

    [Test(Description = "Проверка того, что получение PerWebRequest сервиса внутри скоупа не вызывает исключение")]
    public void GetScopedServiceInstanceDoesNotThrowException_InsideScope()
    {
        using var scope = diInstaller.BeginScope();

        diInstaller.GetInstance<IPerWebRequestClass>().Should().BeOfType<PerWebRequestClass>();
    }

    [Test(Description = "Проверка регистрации класса с наследованием интерфейсов (аналог RedisCheck) - специфичный случай, когда IDerivedCheck : IBaseCheck")]
    public void RegisterClassWithInheritedInterfaces_RegistersUnderBothInterfaces()
    {
        // Act & Assert
        diInstaller.GetInstance<IBaseCheck>().Should().NotBeNull();
        diInstaller.GetInstance<IDerivedCheck>().Should().NotBeNull();
        diInstaller.GetInstance<TestCheck>().Should().NotBeNull();
    }
}
