using System;
using System.Linq;

namespace Moedelo.InfrastructureV2.Domain.Helpers;

public static class TypeExtensions
{
    public static bool IsAssignableFromOrIsAssignableFromGenericType(this Type parent, Type child)
    {
        return parent.IsAssignableFrom(child) || parent.IsAssignableFromGenericType(child);
    }
        
    private static bool IsAssignableFromGenericType(this Type genericType, Type givenType)
    {
        var interfaceTypes = givenType.GetInterfaces();

        if (interfaceTypes.Any(it => it.IsGenericType && it.GetGenericTypeDefinition() == genericType))
        {
            return true;
        }

        if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
        {
            return true;
        }

        var baseType = givenType.BaseType;
        return baseType != null && genericType.IsAssignableFromGenericType(baseType);
    }
}