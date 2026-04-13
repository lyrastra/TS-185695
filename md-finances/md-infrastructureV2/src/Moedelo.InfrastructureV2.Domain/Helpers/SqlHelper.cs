using System;
using System.Collections.Generic;

namespace Moedelo.InfrastructureV2.Domain.Helpers;

public static class SqlHelper
{
    private static readonly Dictionary<Type, string> TypeMap;

    static SqlHelper()
    {
        TypeMap = new Dictionary<Type, string>
        {
            [typeof (string)] = "varchar(max)",
            [typeof (char[])] = "varchar(max)",
            [typeof (byte)] = "tinyint",
            [typeof (short)] = "smallint",
            [typeof (int)] = "int",
            [typeof (long)] = "bigint",
            [typeof (byte[])] = "varbinary(max)",
            [typeof (bool)] = "bit",
            [typeof (DateTime)] = "datetime2",
            [typeof (decimal)] = "decimal(20, 4)",
            [typeof (float)] = "real",
            [typeof (double)] = "float",
            [typeof (TimeSpan)] = "time",
            [typeof (Guid)] = "uniqueidentifier"
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