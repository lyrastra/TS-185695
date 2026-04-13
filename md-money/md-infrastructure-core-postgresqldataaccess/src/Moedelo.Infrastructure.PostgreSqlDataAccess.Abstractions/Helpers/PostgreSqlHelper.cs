using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Moedelo.Infrastructure.PostgreSqlDataAccess.Abstractions.Helpers
{
    public static class PostgreSqlHelper
    {
        private static readonly Dictionary<Type, string> TypeMap;

        static PostgreSqlHelper()
        {
            TypeMap = new Dictionary<Type, string>
            {
                [typeof(string)] = "text",
                [typeof(char[])] = "text",
                [typeof(byte)] = "bit(8)",
                [typeof(short)] = "smallint",
                [typeof(int)] = "integer",
                [typeof(long)] = "bigint",
                [typeof(byte[])] = "bytea",
                [typeof(bool)] = "boolean",
                [typeof(DateTime)] = "timestamp",
                [typeof(DateTimeOffset)] = "timestamptz",
                [typeof(decimal)] = "numeric",
                [typeof(float)] = "real",
                [typeof(double)] = "double precision",
                [typeof(TimeSpan)] = "time",
                [typeof(Guid)] = "uuid",
                [typeof(IPAddress)] = "inet"
            };
        }

        public static Type GetColumnDataType(this Type type)
        {
            Type giveType = type;

            if (giveType.IsGenericType && giveType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                giveType = Nullable.GetUnderlyingType(giveType)
                           ?? throw new Exception($"Unable get underlying type of {giveType.Name}");
            }

            if (giveType.IsEnum)
            {
                giveType = typeof(string);
            }

            return giveType;
        }

        public static string GetDbTypeName(this Type type)
        {
            var giveType = type.GetColumnDataType();
            
            if (TypeMap.TryGetValue(giveType, out var typeName))
            {
                return typeName;
            }

            if (giveType.IsArray)
            {
                var elementType = giveType.GetElementType();

                if (elementType != null && TypeMap.TryGetValue(elementType, out typeName))
                {
                    return $"{typeName}[]";
                }
            }

            if (giveType.IsGenericType && giveType.GetInterfaces().Contains(typeof(IEnumerable)))
            {
                var elementType = giveType.GetGenericArguments();

                if (elementType == null || elementType.Length != 1)
                {
                    throw new ArgumentException($"{type.FullName} is not a supported .NET type");
                }

                if (TypeMap.TryGetValue(elementType[0], out typeName))
                {
                    return $"{typeName}[]";
                }
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
}