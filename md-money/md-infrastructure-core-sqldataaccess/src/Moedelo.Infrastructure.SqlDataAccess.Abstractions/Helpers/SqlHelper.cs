using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Moedelo.Infrastructure.SqlDataAccess.Abstractions.Helpers;

public static class SqlHelper
{
    private static readonly ReadOnlyDictionary<Type, string> TypeMap = new ReadOnlyDictionary<Type, string>(
        new Dictionary<Type, string>
        {
            [typeof(string)] = "varchar(max)",
            [typeof(char[])] = "varchar(max)",
            [typeof(byte)] = "tinyint",
            [typeof(short)] = "smallint",
            [typeof(int)] = "int",
            [typeof(long)] = "bigint",
            [typeof(byte[])] = "varbinary(max)",
            [typeof(bool)] = "bit",
            [typeof(DateTime)] = "datetime2",
            [typeof(decimal)] = "decimal(20, 4)",
            [typeof(float)] = "real",
            [typeof(double)] = "float",
            [typeof(TimeSpan)] = "time",
            [typeof(Guid)] = "uniqueidentifier"
        });

    public static Type GetColumnDataType(this Type type)
    {
        var resultType = type ?? throw new ArgumentNullException(nameof(type));

        if (resultType.IsGenericType && resultType.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            resultType = Nullable.GetUnderlyingType(resultType)
                         ?? throw new ArgumentNullException(nameof(type), $"Неожиданно Nullable.GetUnderlyingType() вернул null для {resultType.Name}");
        }

        if (resultType.IsEnum)
        {
            resultType = resultType.GetEnumUnderlyingType();
        }

        return resultType;
    }

    public static string GetDbTypeName(this Type type)
    {
        var giveType = type.GetColumnDataType();

        if (TypeMap.TryGetValue(giveType, out var name))
        {
            return name;
        }

        throw new ArgumentException($"{type.FullName} is not a supported .NET type");
    }

    public static string GetDbTypeName<T>()
    {
        return GetDbTypeName(typeof(T));
    }

    public static bool CanBeNull(this Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
               || type == typeof(string);
    }
}
