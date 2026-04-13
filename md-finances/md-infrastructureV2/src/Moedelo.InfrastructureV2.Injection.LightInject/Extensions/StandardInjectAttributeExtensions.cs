using System;
using LightInject;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.InfrastructureV2.Injection.Lightinject.Extensions
{
    internal static class StandardInjectAttributeExtensions
    {
        internal static ILifetime ToLifetime(this InjectAttribute attr)
        {
            switch (attr.Lifetime)
            {
                case InjectionLifetime.Transient:
                    return null;
                case InjectionLifetime.PerScope:
                    return new PerScopeLifetime();
                case InjectionLifetime.Singleton:
                    return new PerContainerLifetime();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
