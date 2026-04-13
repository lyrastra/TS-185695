using System;
using LightInject;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;

namespace Moedelo.InfrastructureV2.Injection.Lightinject.Extensions
{
    internal static class InjectAttributeExtensions
    {
        internal static ILifetime ToLifetime(this InjectAttribute attr)
        {
            switch (attr.InjectionType)
            {
                case InjectionType.Transient:
                    return null;
                case InjectionType.PerWebrequest:
                    return new PerScopeLifetime();
                case InjectionType.Singleton:
                    return new PerContainerLifetime();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
