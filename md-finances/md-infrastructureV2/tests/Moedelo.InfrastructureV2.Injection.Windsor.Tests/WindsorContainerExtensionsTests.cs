using System;
using System.Collections.Generic;
using System.Reflection;
using Castle.Core;
using Castle.MicroKernel.Lifestyle;
using Castle.Windsor;
using FluentAssertions;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using NUnit.Framework;

namespace Moedelo.InfrastructureV2.Injection.Windsor.Tests
{
    [TestFixture]
    public class WindsorContainerExtensionsTests
    {
        private IWindsorContainer container;

        [SetUp]
        public void SetUp()
        {
            container = new WindsorContainer();
            container.OverridePerWebRequestLifestyle();
        }

        [TearDown]
        public void TearDown()
        {
            container.Dispose();
        }

        [Test(Description = "Регистрация наследника интерфейса IDI через InjectAsTransient")]
        public void InjectTransientIDI()
        {
            container.RegisterByAttributes(new[]
            {
                Assembly.GetAssembly(this.GetType()),
            });

            var resolved = container.Resolve<ISomeTransientIDI>();

            resolved.Should().BeOfType<SomeTransientIDI>();
        }

        [Test(Description = "Регистрация через InjectAsTransient с указанием типа интерфейса")]
        public void InjectTransient()
        {
            container.RegisterByAttributes(new[]
            {
                Assembly.GetAssembly(this.GetType()),
            });

            var resolved = container.Resolve<ISomeTransient>();

            resolved.Should().BeOfType<SomeTransient>();
        }

        [Test(Description = "Саморегистрация класса наследника IDI через InjectAsTransient")]
        public void InjectSelfTransientIDI()
        {
            container.RegisterByAttributes(new[]
            {
                Assembly.GetAssembly(this.GetType()),
            });

            var resolved = container.Resolve<SelfTransientIDI>();

            resolved.Should().BeOfType<SelfTransientIDI>();
        }

        [Test(Description = "Саморегистрация через InjectAsTransient с указанием собственного типа")]
        public void InjectSelfTransient()
        {
            container.RegisterByAttributes(new[]
            {
                Assembly.GetAssembly(this.GetType()),
            });

            var resolved = container.Resolve<SelfTransient>();

            resolved.Should().BeOfType<SelfTransient>();
        }

        [Test(Description = "Саморегистрация через InjectAsTransient с указанием собственного типа как transient")]
        public void InjectSelfTransientAsTransient()
        {
            container.RegisterByAttributes(new[]
            {
                Assembly.GetAssembly(this.GetType()),
            });

            var resolved1 = container.Resolve<SelfTransient>();
            var resolved2 = container.Resolve<SelfTransient>();

            resolved2.Should().NotBeSameAs(resolved1);
        }

        [Test(Description = "Регистрация наследника интерфейса IDI через InjectPerWebRequest")]
        [Ignore("Не разобрался как в NUnit проекте подключить PerWebRequest модуль Windsor Castle")]
        public void InjectPerWebRequestIDI()
        {
            container.RegisterByAttributes(new[]
            {
                Assembly.GetAssembly(this.GetType()),
            });

            var resolved = container.Resolve<ISomePerWebRequestIDI>();

            resolved.Should().BeOfType<SomePerWebRequestIDI>();
        }

        [Test(Description = "Регистрация через InjectPerWebRequest с указанием типа интерфейса")]
        [Ignore("Не разобрался как в NUnit проекте подключить PerWebRequest модуль Windsor Castle")]
        public void InjectPerWebRequest()
        {
            container.RegisterByAttributes(new[]
            {
                Assembly.GetAssembly(this.GetType()),
            });

            var resolved = container.Resolve<ISomePerWebRequest>();

            resolved.Should().BeOfType<SomePerWebRequest>();
        }

        [Test(Description = "Саморегистрация класса наследника IDI через InjectPerWebRequest")]
        [Ignore("Не разобрался как в NUnit проекте подключить PerWebRequest модуль Windsor Castle")]
        public void InjectSelfPerWebRequestIDI()
        {
            container.RegisterByAttributes(new[]
            {
                Assembly.GetAssembly(this.GetType()),
            });

            var resolved = container.Resolve<SelfPerWebRequestIDI>();

            resolved.Should().BeOfType<SelfPerWebRequestIDI>();
        }

        [Test(Description = "Саморегистрация через InjectPerWebRequest с указанием собственного типа")]
        [Ignore("Не разобрался как в NUnit проекте подключить PerWebRequest модуль Windsor Castle")]
        public void InjectSelfPerWebRequest()
        {
            container.RegisterByAttributes(new[]
            {
                Assembly.GetAssembly(this.GetType()),
            });

            var resolved = container.Resolve<SelfPerWebRequest>();

            resolved.Should().BeOfType<SelfPerWebRequest>();
        }

        [Test(Description = "Регистрация наследника интерфейса IDI через InjectAsSingleton")]
        public void InjectSingletonIDI()
        {
            container.RegisterByAttributes(new[]
            {
                Assembly.GetAssembly(this.GetType()),
            });

            var resolved = container.Resolve<ISomeSingletonIDI>();

            resolved.Should().BeOfType<SomeSingletonIDI>();
        }

        [Test(Description = "Регистрация через InjectAsSingleton с указанием типа интерфейса")]
        public void InjectSingleton()
        {
            container.RegisterByAttributes(new[]
            {
                Assembly.GetAssembly(this.GetType()),
            });

            var resolved = container.Resolve<ISomeSingleton>();

            resolved.Should().BeOfType<SomeSingleton>();
        }

        [Test(Description = "Саморегистрация класса наследника IDI через InjectAsSingleton")]
        public void InjectSelfSingletonIDI()
        {
            container.RegisterByAttributes(new[]
            {
                Assembly.GetAssembly(this.GetType()),
            });

            var resolved = container.Resolve<SelfSingletonIDI>();

            resolved.Should().BeOfType<SelfSingletonIDI>();
        }

        [Test(Description = "Саморегистрация через InjectAsSingleton с указанием собственного типа")]
        public void InjectSelfSingleton()
        {
            container.RegisterByAttributes(new[]
            {
                Assembly.GetAssembly(this.GetType()),
            });

            var resolved = container.Resolve<SelfSingleton>();

            resolved.Should().BeOfType<SelfSingleton>();
        }
        
        [Test(Description = "Саморегистрация через InjectAsSingleton с указанием собственного типа регистрирует синглетон")]
        public void InjectSelfSingletonAsSingleton()
        {
            container.RegisterByAttributes(new[]
            {
                Assembly.GetAssembly(this.GetType()),
            });

            var resolved1 = container.Resolve<SelfSingleton>();
            var resolved2 = container.Resolve<SelfSingleton>();

            resolved2.Should().BeSameAs(resolved1);
        }
    }

    public interface ISomeTransientIDI : IDI {}

    [InjectAsTransient]
    public class SomeTransientIDI : ISomeTransientIDI {}

    public interface ISomeTransient {}

    [InjectAsTransient(typeof(ISomeTransient))]
    public class SomeTransient : ISomeTransient {}

    [InjectAsTransient]
    public class SelfTransientIDI : IDI {}
    
    [InjectAsTransient(typeof(SelfTransient))]
    public class SelfTransient {}

    public interface ISomeSingletonIDI : IDI {}

    [InjectAsSingleton]
    public class SomeSingletonIDI : ISomeSingletonIDI {}
    
    public interface ISomeSingleton {}

    [InjectAsSingleton(typeof(ISomeSingleton))]
    public class SomeSingleton : ISomeSingleton {}

    [InjectAsSingleton]
    public class SelfSingletonIDI : IDI {}
    
    [InjectAsSingleton(typeof(SelfSingleton))]
    public class SelfSingleton {}
    
    public interface ISomePerWebRequestIDI : IDI {}

    [InjectPerWebRequest]
    public class SomePerWebRequestIDI : ISomePerWebRequestIDI {}
    
    public interface ISomePerWebRequest : IDI {}

    [InjectPerWebRequest(typeof(ISomePerWebRequest))]
    public class SomePerWebRequest : ISomePerWebRequest {}

    [InjectPerWebRequest]
    public class SelfPerWebRequestIDI : IDI {}

    [InjectPerWebRequest(typeof(SelfPerWebRequest))]
    public class SelfPerWebRequest {}

    
    public static class WindsorContainerExtensions
    {
        public static IWindsorContainer OverridePerWebRequestLifestyle(this IWindsorContainer container)
        {
            container.Kernel.ComponentModelCreated += model =>
            {
                if (model.IsPerWebRequestLifestyle())
                {
                    model.LifestyleType = LifestyleType.Transient;
                }
            };

            return container;
        }

        private static bool IsPerWebRequestLifestyle(this ComponentModel model)
        {
            return model.LifestyleType == LifestyleType.Scoped
                   && model.HasAccessorType(typeof(WebRequestScopeAccessor));
        }

        private static bool HasAccessorType(this ComponentModel model, Type type)
            => model.HasExtendedProperty("castle.scope-accessor-type", type);

        private static bool HasExtendedProperty<T>(this ComponentModel model, object key, T expected)
        {
            return model.ExtendedProperties[key] is T actual
                   && EqualityComparer<T>.Default.Equals(actual, expected);
        }
    }
}
