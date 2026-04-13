using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Moedelo.Infrastructure.System.Extensions.Types;

public static class TypeExtensions
{
    public class TypeComparisonResult
    {
        public Type T1 { get; internal set; }
        public Type T2 { get; internal set; }
        public List<PropertyInfo> ExtraFieldsOfT1 { get; } = new List<PropertyInfo>();
        public List<PropertyInfo> ExtraFieldsOfT2 { get; } = new List<PropertyInfo>();
        public bool AreTypesCompatible { get; internal set; }
        public static TypeComparisonResult Success(Type T1, Type T2) => new TypeComparisonResult {T1 = T1, T2 = T2, AreTypesCompatible = true};
    }

    public class TypesAreIncompatibleException : Exception
    {
        public TypesAreIncompatibleException(Type T1, Type T2)
            : base($">>>>> Types {T1} and {T2} are incompatible when it is required <<<<<")
        {
        }
    }

    public static TypeComparisonResult CompareTo(this Type T1, Type T2)
    {
        if (T1 == T2)
        {
            return new TypeComparisonResult {T1 = T1, T2 = T2, AreTypesCompatible = true};
        }

        if (T1.IsEnum)
        {
            return new TypeComparisonResult { T1 = T1, T2 = T2, AreTypesCompatible = T2 == typeof(int) || IsCompatibleEnums(T1, T2) };
        }

        if (T2.IsEnum)
        {
            return new TypeComparisonResult { T1 = T1, T2 = T2, AreTypesCompatible = T1 == typeof(int) };
        }

        if (T1.IsPrimitive || T2.IsPrimitive)
        {
            // остальные примитивные типы считаем несовместимыми
            return new TypeComparisonResult { T1 = T1, T2 = T2, AreTypesCompatible = false };
        }

        if (T1 == typeof(string) || T2 == typeof(string))
        {
            // string - сложный тип. Но он не совместим ни с одним другим сложным типом
            return new TypeComparisonResult { T1 = T1, T2 = T2, AreTypesCompatible = false };
        }

        var props1 = T1.GetProperties().ToDictionary(p => p.Name, p => p);
        var props2 = T2.GetProperties().ToDictionary(p => p.Name, p => p);

        var result = new TypeComparisonResult { T1 = T1, T2 = T2 };

        result.ExtraFieldsOfT1.AddRange(props1
            .Where(p1 => !props2.ContainsKey(p1.Key) || !p1.Value.PropertyType.IsCompatibleTo(props2[p1.Key].PropertyType))
            .Select(p => p.Value));

        result.ExtraFieldsOfT2.AddRange(props2
            .Where(p2 => !props1.ContainsKey(p2.Key) || !p2.Value.PropertyType.IsCompatibleTo(props1[p2.Key].PropertyType))
            .Select(p => p.Value));

        result.AreTypesCompatible = !result.ExtraFieldsOfT1.Any() && !result.ExtraFieldsOfT2.Any();

        return result;
    }

    public static void EnsureCompatibleTo(this Type T1, Type T2)
    {
        if (!T1.IsCompatibleTo(T2))
        {
            throw new
                TypesAreIncompatibleException(T1, T2);
        }
    }

    public static bool IsCompatibleTo(this Type T1, Type T2)
    {
        var comparison = T1.CompareTo(T2);

        return comparison.AreTypesCompatible;
    }

    public static bool IsCompatibleEnums(this Type T1, Type T2)
    {
        if (T1.IsEnum && T2.IsEnum)
        {
            var values1 = Enum.GetValues(T1);
            var values2 = Enum.GetValues(T2);

            return values1.Cast<int>().SequenceEqual(values2.Cast<int>());
        }

        return false;
    }
}