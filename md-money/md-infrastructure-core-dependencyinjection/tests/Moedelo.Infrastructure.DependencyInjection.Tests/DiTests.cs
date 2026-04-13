using System;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Linq;
using FluentAssertions;

namespace Moedelo.Infrastructure.DependencyInjection.Tests;

[TestFixture]
public class DiTests
{
    private IServiceProvider serviceProvider;

    [SetUp]
    public void Setup()
    {
        var serviceCollection = new ServiceCollection();
        var di = new DIInstaller(serviceCollection);
        di.RegisterByDIAttribute(typeof(DiTests).Assembly);
        di.Initialize();
        serviceProvider = serviceCollection.BuildServiceProvider();
    }

    [TearDown]
    public void TearDown()
    {
        (serviceProvider as IDisposable)?.Dispose();
    }

    #region Basic Registration Tests

    [Test, Category("BasicRegistration")]
    public void InterfaceWithoutMatchingImplementation_ShouldNotBeRegistered()
    {
        // Act
        var actual = serviceProvider.GetService<IWithoutImplementation>();

        // Assert
        actual.Should().BeNull();
    }

    [Test, Category("BasicRegistration")]
    public void ConcreteClassWithExplicitType_ShouldBeRegistered()
    {
        // Act
        var actual = serviceProvider.GetService<WithoutImplementationWithAttribute>();
        
        // Assert
        actual.Should().NotBeNull()
            .And.BeOfType<WithoutImplementationWithAttribute>();
    }

    [Test, Category("BasicRegistration")]
    public void InterfaceWithExplicitImplementation_ShouldBeRegistered()
    {
        // Act
        var actual = serviceProvider.GetService<IWithImplementation>();

        // Assert
        actual.Should().NotBeNull()
            .And.BeOfType<WithImplementation>();
    }

    [Test, Category("BasicRegistration")]
    public void AutomaticInterfaceDiscovery_WhenNoTypeSpecified_ShouldRegisterMoedeloInterfaces()
    {
        // Act
        var actual = serviceProvider.GetService<IWithImplementationWithoutTypeInAttribute>();

        // Assert
        actual.Should().NotBeNull()
            .And.BeOfType<WithImplementationWithoutTypeInAttribute>();
    }

    [Test, Category("BasicRegistration")]
    public void AutomaticInterfaceDiscovery_WhenMultipleInterfaces_ShouldRegisterAllMoedeloInterfaces()
    {
        // Act
        var actualOne = serviceProvider.GetService<IWithImplementationWithoutTypeInAttributeOne>();
        var actualTwo = serviceProvider.GetService<IWithImplementationWithoutTypeInAttributeTwo>();

        // Assert
        actualOne.Should().NotBeNull()
            .And.BeOfType<WithImplementationWithoutTypeInAttributeOneTwo>();
        actualTwo.Should().NotBeNull()
            .And.BeOfType<WithImplementationWithoutTypeInAttributeOneTwo>();
    }

    #endregion

    #region Multiple Implementations Tests

    [Test, Category("MultipleImplementations")]
    public void MultipleImplementations_WithoutAttribute_ShouldRegisterOnlyLastOne()
    {
        // Act
        var actualOne = serviceProvider.GetService<IMultipleNotPossibleWithImplementationWithoutTypeInAttribute>();
        var actualAll = serviceProvider.GetServices<IMultipleNotPossibleWithImplementationWithoutTypeInAttribute>().ToArray();

        // Assert
        actualOne.Should().NotBeNull()
            .And.BeOfType<MultipleNotPossibleWithImplementationWithoutTypeInAttributeTwo>();
        
        actualAll.Should().HaveCount(1);
        actualAll[0].Should().BeOfType<MultipleNotPossibleWithImplementationWithoutTypeInAttributeTwo>();
    }

    [Test, Category("MultipleImplementations")]
    public void MultipleImplementations_WithAttribute_ShouldRegisterAllImplementations()
    {
        // Act
        var actual = serviceProvider.GetServices<IMultiplePossibleWithImplementationWithoutTypeInAttribute>().ToArray();

        // Assert
        actual.Should().HaveCount(2);
        actual[0].Should().BeOfType<MultiplePossibleWithImplementationWithoutTypeInAttributeOne>();
        actual[1].Should().BeOfType<MultiplePossibleWithImplementationWithoutTypeInAttributeTwo>();
    }

    #endregion

    #region Factory Registration Tests

    [Test, Category("FactoryRegistration")]
    public void FactoryRegistration_TransientWithFactory_ShouldRegisterBothServiceAndFactory()
    {
        // Act
        var service = serviceProvider.GetService<ITransientServiceWithFactory>();
        var factory = serviceProvider.GetService<Func<ITransientServiceWithFactory>>();

        // Assert
        service.Should().NotBeNull()
            .And.BeOfType<TransientServiceWithFactory>();
        factory.Should().NotBeNull();
        
        // Verify factory works
        var instanceFromFactory = factory();
        instanceFromFactory.Should().NotBeNull()
            .And.BeOfType<TransientServiceWithFactory>();
    }

    [Test, Category("FactoryRegistration")]
    public void FactoryRegistration_TransientWithoutFactory_ShouldRegisterOnlyService()
    {
        // Act
        var service = serviceProvider.GetService<ITransientServiceWithoutFactory>();
        var factory = serviceProvider.GetService<Func<ITransientServiceWithoutFactory>>();

        // Assert
        service.Should().NotBeNull();
        factory.Should().BeNull();
    }

    [Test, Category("FactoryRegistration")]
    public void FactoryRegistration_Transient_FactoryShouldReturnNewInstancesEachTime()
    {
        // Arrange
        var factory = serviceProvider.GetRequiredService<Func<ITransientServiceWithFactory>>();

        // Act
        var instance1 = factory();
        var instance2 = factory();
        var instance3 = factory();

        // Assert
        instance1.Should().NotBeNull();
        instance2.Should().NotBeNull();
        instance3.Should().NotBeNull();
        
        instance1.Should().NotBeSameAs(instance2);
        instance2.Should().NotBeSameAs(instance3);
        instance1.Should().NotBeSameAs(instance3);
    }

    [Test, Category("FactoryRegistration")]
    public void FactoryRegistration_ScopedWithFactory_ShouldRegisterBothServiceAndFactory()
    {
        // Act & Assert
        using (var scope = serviceProvider.CreateScope())
        {
            var service = scope.ServiceProvider.GetService<IScopedServiceWithFactory>();
            var factory = scope.ServiceProvider.GetService<Func<IScopedServiceWithFactory>>();

            service.Should().NotBeNull()
                .And.BeOfType<ScopedServiceWithFactory>();
            factory.Should().NotBeNull();
            
            // Verify factory works
            var instanceFromFactory = factory();
            instanceFromFactory.Should().NotBeNull()
                .And.BeOfType<ScopedServiceWithFactory>();
        }
    }

    [Test, Category("FactoryRegistration")]
    public void FactoryRegistration_Scoped_FactoryShouldRespectScopeLifetime()
    {
        // Arrange & Act
        IScopedServiceWithFactory instance1FromScope1;
        IScopedServiceWithFactory instance2FromScope1;

        // В одном scope - тот же экземпляр
        using (var scope1 = serviceProvider.CreateScope())
        {
            var factory = scope1.ServiceProvider.GetRequiredService<Func<IScopedServiceWithFactory>>();
            instance1FromScope1 = factory();
            instance2FromScope1 = factory();
            
            // Assert - same scope returns same instance
            instance1FromScope1.Should().BeSameAs(instance2FromScope1,
                "factory should return same instance within same scope");
        }

        IScopedServiceWithFactory instance1FromScope2;

        // В другом scope - другой экземпляр
        using (var scope2 = serviceProvider.CreateScope())
        {
            var factory = scope2.ServiceProvider.GetRequiredService<Func<IScopedServiceWithFactory>>();
            instance1FromScope2 = factory();
            
            // Assert - different scope returns different instance
            instance1FromScope2.Should().NotBeSameAs(instance1FromScope1,
                "factory should return different instance in different scope");
        }
    }

    #endregion

    #region Lifetime Verification Tests

    [Test, Category("Lifetime")]
    public void SingletonService_ShouldReturnSameInstanceEveryTime()
    {
        // Act
        var instance1 = serviceProvider.GetRequiredService<IWithImplementation>();
        var instance2 = serviceProvider.GetRequiredService<IWithImplementation>();
        var instance3 = serviceProvider.GetRequiredService<IWithImplementation>();

        // Assert
        instance1.Should().BeSameAs(instance2);
        instance2.Should().BeSameAs(instance3);
    }

    [Test, Category("Lifetime")]
    public void TransientService_ShouldReturnNewInstanceEveryTime()
    {
        // Act
        var instance1 = serviceProvider.GetRequiredService<ITransientServiceWithFactory>();
        var instance2 = serviceProvider.GetRequiredService<ITransientServiceWithFactory>();
        var instance3 = serviceProvider.GetRequiredService<ITransientServiceWithFactory>();

        // Assert
        instance1.Should().NotBeSameAs(instance2);
        instance2.Should().NotBeSameAs(instance3);
        instance1.Should().NotBeSameAs(instance3);
    }

    [Test, Category("Lifetime")]
    public void ScopedService_ShouldReturnSameInstanceWithinScope_DifferentAcrossScopes()
    {
        // Arrange & Act
        IScopedServiceWithFactory instanceFromScope1;
        IScopedServiceWithFactory anotherInstanceFromScope1;

        using (var scope1 = serviceProvider.CreateScope())
        {
            instanceFromScope1 = scope1.ServiceProvider.GetRequiredService<IScopedServiceWithFactory>();
            anotherInstanceFromScope1 = scope1.ServiceProvider.GetRequiredService<IScopedServiceWithFactory>();
            
            // Assert - same within scope
            instanceFromScope1.Should().BeSameAs(anotherInstanceFromScope1);
        }

        IScopedServiceWithFactory instanceFromScope2;

        using (var scope2 = serviceProvider.CreateScope())
        {
            instanceFromScope2 = scope2.ServiceProvider.GetRequiredService<IScopedServiceWithFactory>();
            
            // Assert - different across scopes
            instanceFromScope2.Should().NotBeSameAs(instanceFromScope1);
        }
    }

    #endregion

    #region Test Fixtures

    private interface IWithoutImplementation { }

    [InjectAsSingleton(typeof(IWithoutImplementation))]
    private class WithoutImplementation { }


    [InjectAsSingleton(typeof(WithoutImplementationWithAttribute))]
    private class WithoutImplementationWithAttribute { }


    private interface IWithImplementation { }

    [InjectAsSingleton(typeof(IWithImplementation))]
    private class WithImplementation : IWithImplementation { }


    private interface IWithImplementationWithoutTypeInAttribute { }

    [InjectAsSingleton]
    private class WithImplementationWithoutTypeInAttribute : IWithImplementationWithoutTypeInAttribute { }

    private interface IWithImplementationWithoutTypeInAttributeOne { }

    private interface IWithImplementationWithoutTypeInAttributeTwo { }

    [InjectAsSingleton]
    private class WithImplementationWithoutTypeInAttributeOneTwo : IWithImplementationWithoutTypeInAttributeOne, IWithImplementationWithoutTypeInAttributeTwo { }


    private interface IMultipleNotPossibleWithImplementationWithoutTypeInAttribute { }

    [InjectAsSingleton(typeof(IMultipleNotPossibleWithImplementationWithoutTypeInAttribute))]
    private class MultipleNotPossibleWithImplementationWithoutTypeInAttributeOne : IMultipleNotPossibleWithImplementationWithoutTypeInAttribute { }

    [InjectAsSingleton(typeof(IMultipleNotPossibleWithImplementationWithoutTypeInAttribute))]
    private class MultipleNotPossibleWithImplementationWithoutTypeInAttributeTwo : IMultipleNotPossibleWithImplementationWithoutTypeInAttribute { }


    [MultipleImplementationsPossible]
    private interface IMultiplePossibleWithImplementationWithoutTypeInAttribute { }

    [InjectAsSingleton(typeof(IMultiplePossibleWithImplementationWithoutTypeInAttribute))]
    private class MultiplePossibleWithImplementationWithoutTypeInAttributeOne : IMultiplePossibleWithImplementationWithoutTypeInAttribute { }

    [InjectAsSingleton(typeof(IMultiplePossibleWithImplementationWithoutTypeInAttribute))]
    private class MultiplePossibleWithImplementationWithoutTypeInAttributeTwo : IMultiplePossibleWithImplementationWithoutTypeInAttribute { }


    // Factory registration test classes

    private interface ITransientServiceWithFactory { }

    [InjectAsTransient(typeof(ITransientServiceWithFactory), registerFactory: true)]
    private class TransientServiceWithFactory : ITransientServiceWithFactory { }


    private interface ITransientServiceWithoutFactory { }

    [InjectAsTransient(typeof(ITransientServiceWithoutFactory), registerFactory: false)]
    private class TransientServiceWithoutFactory : ITransientServiceWithoutFactory { }


    private interface IScopedServiceWithFactory { }

    [InjectPerScope(typeof(IScopedServiceWithFactory), registerFactory: true)]
    private class ScopedServiceWithFactory : IScopedServiceWithFactory { }

    #endregion
}
