#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Helpers;
using Moedelo.InfrastructureV2.Injection.Lightinject.Exceptions;
using StandardInjectAttribute = Moedelo.Infrastructure.DependencyInjection.Abstractions.InjectAttribute;

namespace Moedelo.InfrastructureV2.Injection.Lightinject.Extensions;

internal static class TypeExtensions
{
    /// <summary>
    /// Получение информации о внедрении для типа, который реализует <see cref="InjectAttribute"/>
    /// из <see cref="Moedelo.InfrastructureV2.Domain.Attributes.Injection"/>
    /// </summary>
    /// <param name="implementationType"></param>
    /// <param name="injectionMarkerType"></param>
    /// <returns></returns>
    /// <exception cref="RuntimeMoedeloDependencyInjectionException"></exception>
    internal static TypeInjectionSetup? GetV2InjectionInfo(this Type implementationType, Type injectionMarkerType)
    {
        var injectAttribute = implementationType.GetCustomAttribute<InjectAttribute>(false);

        if (injectAttribute == null)
        {
            return null;
        }

        if (injectAttribute.AbstractType != null
            && !injectAttribute.AbstractType.IsAssignableFromOrIsAssignableFromGenericType(implementationType))
        {
            throw new RuntimeMoedeloDependencyInjectionException(implementationType, injectAttribute.AbstractType);
        }

        foreach (var injectionTargetType in injectAttribute.AbstractTypes)
        {
            if (!injectionTargetType.IsAssignableFromOrIsAssignableFromGenericType(implementationType))
            {
                throw new RuntimeMoedeloDependencyInjectionException(implementationType, injectionTargetType);
            }
        }

        return new TypeInjectionSetup
        {
            Implementation = implementationType,
            Lifetime = injectAttribute.ToLifetime(),
            InjectionTargetServices = CollectInjectionTargetServices(
                    implementationType,
                    injectAttribute.AbstractType,
                    injectAttribute.AbstractTypes,
                    injectionMarkerType)
                .Distinct()
                .ToArray() 
        };
    }

    /// <summary>
    /// Получение информации о внедрении для типа, который реализует <see cref="StandardInjectAttribute"/>
    /// из <see cref="Moedelo.Infrastructure.DependencyInjection.Abstractions"/>
    /// </summary>
    /// <param name="implementationType"></param>
    /// <returns></returns>
    /// <exception cref="RuntimeMoedeloDependencyInjectionException"></exception>
    internal static TypeInjectionSetup? GetStandardInjectionInfo(this Type implementationType)
    {
        var injectAttribute = implementationType.GetCustomAttribute<StandardInjectAttribute>(false);

        if (injectAttribute == null)
        {
            return null;
        }

        var injectionTargetService = injectAttribute.AbstractType;

        if (!injectionTargetService.IsAssignableFromOrIsAssignableFromGenericType(implementationType))
        {
            throw new RuntimeMoedeloDependencyInjectionException(implementationType, injectionTargetService);
        }

        return new TypeInjectionSetup
        {
            Implementation = implementationType,
            Lifetime = injectAttribute.ToLifetime(),
            InjectionTargetServices = new []{injectionTargetService} 
        };
    }

    internal static string GetTypeLoggingName(this Type type)
    {
        if (!string.IsNullOrWhiteSpace(type.FullName))
        {
            return type.FullName;
        }

        if (!string.IsNullOrWhiteSpace(type.Name))
        {
            return type.Name;
        }

        return type.ToString();
    }

    private static IEnumerable<Type> CollectInjectionTargetServices(Type implementationType,
        Type? injectionTargetService,
        Type[] injectionTargetServices,
        Type injectionMarkerType)
    {
        // внедряем сам тип
        yield return implementationType;

        if (injectionTargetService != null
            && injectionTargetService != implementationType
            && injectionMarkerType != injectionTargetService)
        {
            // внедряем явно указанный целевой тип
            yield return injectionTargetService;
        }

        foreach (var type in injectionTargetServices)
        {
            yield return type;
        }

        // внедряем интерфейсы, которые наследуются от указанного типа-маркера внедрения
        foreach(var targetType in implementationType.GetInterfaces()
                    .Select(typeInterface => typeInterface.GetInjectionTargetService())
                    .Where(injectionMarkerType.IsAssignableFromOrIsAssignableFromGenericType))
        {
            yield return targetType;
        }
    }

    private static Type GetInjectionTargetService(this Type type)
    {
        return type is { IsInterface: true, IsGenericType: true, FullName: null }
            ? type.GetGenericTypeDefinition()
            : type;
    }
}