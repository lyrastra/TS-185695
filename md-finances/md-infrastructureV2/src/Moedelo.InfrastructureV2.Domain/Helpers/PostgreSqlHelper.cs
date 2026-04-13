using System;
using System.Collections.Generic;

namespace Moedelo.InfrastructureV2.Domain.Helpers;

public static class PostgreSqlHelper
{
    private static readonly Dictionary<Type, string> TypeMap;

    static PostgreSqlHelper()
    {
        TypeMap = new Dictionary<Type, string>
        {
            [typeof (string)] = "varchar",
            [typeof (char[])] = "varchar",
            [typeof (byte)] = "tinyint",
            [typeof (short)] = "smallint",
            [typeof (int)] = "int",
            [typeof (long)] = "bigint",
            [typeof (byte[])] = "bytea",
            [typeof (bool)] = "bool",
            [typeof (DateTime)] = "timestamp",
            [typeof (decimal)] = "decimal(20, 4)",
            [typeof (float)] = "float4",
            [typeof (double)] = "float8",
            [typeof (TimeSpan)] = "time",
            [typeof (Guid)] = "uuid"
        };
    }

    public static string GetDbTypeName(Type type)
    {
        Type giveType = type;

        if (giveType.IsGenericType && giveType.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            giveType = Nullable.GetUnderlyingType(giveType);
        }

        if (null != giveType)
        {

            if (giveType.IsEnum)
            {
                giveType = giveType.GetEnumUnderlyingType();
            }

            if (TypeMap.ContainsKey(giveType))
            {
                return TypeMap[giveType];
            }
        }

        throw new ArgumentException($"{type.FullName} is not a supported .NET type");
    }

    public static string GetDbTypeName<T>()
    {
        return GetDbTypeName(typeof(T));
    }
}