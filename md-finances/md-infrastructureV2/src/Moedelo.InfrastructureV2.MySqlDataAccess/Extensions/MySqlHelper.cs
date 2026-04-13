using System;
using System.Collections.Generic;

namespace Moedelo.InfrastructureV2.MySqlDataAccess.Extensions;

public static class MySqlHelper
{
    private static readonly Dictionary<Type, string> TypeMap;

    static MySqlHelper()
    {
        TypeMap = new Dictionary<Type, string>
        {
            [typeof(string)] = "text",
            [typeof(char[])] = "text",
            [typeof(byte)] = "tinyint",
            [typeof(short)] = "smallint",
            [typeof(int)] = "int",
            [typeof(long)] = "bigint",
            [typeof(byte[])] = "varbinary(max)",
            [typeof(bool)] = "bit",
            [typeof(DateTime)] = "datetime",
            [typeof(decimal)] = "decimal(20, 4)",
            [typeof(float)] = "real",
            [typeof(double)] = "float",
            [typeof(TimeSpan)] = "time",
            [typeof(Guid)] = "char(36)"
        };
    }

    public static Type GetColumnDataType(this Type type)
    {
        Type giveType = type;

        if (giveType.IsGenericType && giveType.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            giveType = Nullable.GetUnderlyingType(giveType);
        }

        if (giveType.IsEnum)
        {
            giveType = giveType.GetEnumUnderlyingType();
        }

        return giveType;
    }

    public static string GetDbTypeName(this Type type)
    {
        Type giveType = type.GetColumnDataType();

        if (TypeMap.ContainsKey(giveType))
        {
            return TypeMap[giveType];
        }

        throw new ArgumentException($"{type.FullName} is not a supported .NET type");
    }
        
    public static string GetDbTypeName<T>()
    {
        return GetDbTypeName(typeof(T));
    }

    public static bool CanBeNull(this Type type)
    {
        return (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
               || type == typeof(string);
    }
}