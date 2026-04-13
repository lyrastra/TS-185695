using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Moedelo.Changelog.Shared.Domain.Attributes;
using Moedelo.Changelog.Shared.Domain.Definitions;

namespace Moedelo.Changelog.Shared.Domain.Extensions
{
    public static class EntityFieldDefinitionExtensions
    {
        public static EntityFieldDefinition FindFieldDefinition(this EntityFieldDefinition definition, string fieldName)
        {
            EntityFieldDefinition fieldDefinition = null;

            return definition?.Fields?.TryGetValue(fieldName, out fieldDefinition) == true
                ? fieldDefinition
                : null;
        }

        public static IReadOnlyDictionary<string, EntityFieldDefinition> ToReadOnlyDictionary(
            this IEnumerable<EntityFieldDefinition> definitions)
        {
            return definitions
                .ToDictionary(definition => definition.Name, definition => definition);
        }

        public static IReadOnlyDictionary<string, EntityFieldDefinition> ToFieldDefinitions(this Type type)
        {
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            
            return properties
                .Select(property => property.ToEntityFieldDefinition())
                .ToDictionary(definition => definition.Name);
        }

        private static EntityFieldDefinition ToEntityFieldDefinition(this PropertyInfo propertyInfo)
        {
            var displayNameAttribute = propertyInfo.GetCustomAttribute<DisplayAttribute>();

            var formatValue = GetFormatValueFunc(propertyInfo.PropertyType);
            var fullStatePropertyName = GetFullStatePropertyName(propertyInfo.PropertyType);

            return new EntityFieldDefinition
            {
                Name = propertyInfo.Name,
                ViewName = displayNameAttribute?.Name,
                FieldType = propertyInfo.GetEntityFieldDefinitionFieldType(),
                PropertyType = propertyInfo.PropertyType,
                Fields = propertyInfo.GetEntityFieldDefinitionFields(),
                ValueFormatter = formatValue,
                FullStatePropertyName = fullStatePropertyName
            };
        }
        
        private static Func<string, string> GetFormatValueFunc(Type type)
        {
            if (type.IsEnum)
            {
                return CreateValueFormatterForEnum(type);
            }

            // nullable enum
            if (type.IsGenericType &&
                type.GetGenericTypeDefinition() == typeof(Nullable<>) &&
                type.GetGenericArguments()[0].IsEnum)
            {
                return CreateValueFormatterForEnum(type.GetGenericArguments()[0]);
            }

            return null;
        }

        private static string GetFullStatePropertyName(Type type)
        {
            if (type == typeof(MoneySum))
            {
                return nameof(MoneySum.ValueAndCurrency);
            }

            return null;
        }

        private static Func<string, string> CreateValueFormatterForEnum(Type enumType)
        {
            var enumInfo = Enum
                .GetNames(enumType)
                .Select(name => new
                {
                    Name = name,
                    IntValue = ((int)Enum.Parse(enumType, name)).ToString()
                })
                .ToArray();
                
            var map = enumInfo
                .Select(info => new {Key = info.IntValue, Name = info.Name, Value = info.IntValue})
                .Concat(enumInfo.Select(info => new {Key = info.Name, Name = info.Name, Value = info.IntValue}))
                .ToDictionary(
                    pair => pair.Key,
                    pair =>
                    {
                        var member = enumType
                            .GetMember(pair.Name)
                            .FirstOrDefault(m => m.DeclaringType == enumType);
                        var attribute = member?.GetCustomAttribute<DisplayAttribute>();

                        var value = attribute?.Name;
                        
                        return value ?? pair.Key.ToString();
                    });

            return enumValue =>
            {
                if (enumValue == null)
                {
                    return null;
                }

                if (map.TryGetValue(enumValue, out var name))
                {
                    return name;
                }

                return  enumValue;
            };
        }

        private static string GetEntityFieldDefinitionFieldType(this PropertyInfo propertyInfo)
        {
            var explicitDeclaration = propertyInfo.GetCustomAttribute<FieldTypeAttribute>();

            if (explicitDeclaration != null)
            {
                return explicitDeclaration.FieldType;
            }

            if (typeof(bool) == propertyInfo.PropertyType || typeof(bool?) == propertyInfo.PropertyType)
            {
                return FieldTypes.Bool;
            }

            if (typeof(MoneySum) == propertyInfo.PropertyType)
            {
                return FieldTypes.Money;
            }

            if (typeof(DateTime) == propertyInfo.PropertyType || typeof(DateTime?) == propertyInfo.PropertyType)
            {
                return FieldTypes.Date;
            }

            if (typeof(decimal) == propertyInfo.PropertyType || typeof(decimal?) == propertyInfo.PropertyType)
            {
                return FieldTypes.Number;
            }

            if (typeof(int) == propertyInfo.PropertyType || typeof(int?) == propertyInfo.PropertyType)
            {
                return FieldTypes.Integer;
            }

            return null;
        }

        private static IReadOnlyDictionary<string, EntityFieldDefinition> GetEntityFieldDefinitionFields(
            this PropertyInfo propertyInfo)
        {
            if (propertyInfo.PropertyType == typeof(string))
            {
                return null;
            }

            if (propertyInfo.PropertyType == typeof(MoneySum))
            {
                return null;
            }

            var itemType = propertyInfo.PropertyType.GetEnumerableType();

            return itemType?.ToFieldDefinitions() ?? propertyInfo.PropertyType.GetClassFieldsDefinition();
        }

        private static IReadOnlyDictionary<string, EntityFieldDefinition> GetClassFieldsDefinition(
            this Type type)
        {
            if (!type.IsClass)
            {
                return null;
            }

            return type.ToFieldDefinitions();
        }

        private static Type GetEnumerableType(this Type type)
        {
            if (type.IsInterface && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                return type.GetGenericArguments()[0];
            }
            
            return type.GetInterfaces()
                .Where(intType => intType.IsGenericType
                                  && intType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                .Select(intType => intType.GetGenericArguments()[0])
                .FirstOrDefault();
        }
    }
}
