using FluentAssertions;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.InfrastructureV2.Injection.Lightinject;
using Moq;
using NUnit.Framework;

namespace Moedelo.InfrastructureV2.Injection.LightInject.Tests;

[TestFixture]
[Parallelizable(ParallelScope.None)]
public class DecorationTests
{
    private readonly Mock<ILogger> loggerMock = new();

    public interface IFoo;
    internal sealed class FooImplementation : IFoo;

    internal sealed class FooDecorator : IFoo
    {
        public FooDecorator(IFoo foo)
        {
            this.Decorated = foo;
        }

        public IFoo Decorated { get; }
    }


    internal class DecorateSingletonDiInstaller : DIInstaller
    {
        public DecorateSingletonDiInstaller(ILogger logger) : base(logger)
        {
        }
    
        protected override void RegisterBehaviour()
        {
            RegisterSingleton<IFoo, FooImplementation>();
            RegisterDecorator<IFoo, FooDecorator>();
        }
    }

    internal class DecorateTransientDiInstaller : DIInstaller
    {
        public DecorateTransientDiInstaller(ILogger logger) : base(logger)
        {
        }
    
        protected override void RegisterBehaviour()
        {
            RegisterTransient<IFoo, FooImplementation>();
            RegisterDecorator<IFoo, FooDecorator>();
        }
    }

    [Test]
    public void DecorateSingleton()
    {
        using var diInstaller = new DecorateSingletonDiInstaller(loggerMock.Object);
        diInstaller.Initialize();

        var foo = diInstaller.GetInstance<IFoo>();
        foo.Should().NotBeNull();
        foo.Should().BeOfType<FooDecorator>();
        foo.As<FooDecorator>().Decorated.Should().NotBeNull();
        foo.As<FooDecorator>().Decorated.Should().BeOfType<FooImplementation>();
    }

    [Test]
    public void DecorateTransient()
    {
        using var diInstaller = new DecorateTransientDiInstaller(loggerMock.Object);
        diInstaller.Initialize();

        var foo = diInstaller.GetInstance<IFoo>();
        foo.Should().NotBeNull();
        foo.Should().BeOfType<FooDecorator>();
        foo.As<FooDecorator>().Decorated.Should().NotBeNull();
        foo.As<FooDecorator>().Decorated.Should().BeOfType<FooImplementation>();
    }
}
