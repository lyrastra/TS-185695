using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Moedelo.CommonV2.Cells.Extensions
{
    /// <summary>
    /// Класс-расширение для работы с информацией об аттрибуте
    /// </summary>
    public static class PropertyInfoExtension
    {
        /// <summary>
        /// Типы System.Collections
        /// </summary>
        private static readonly List<Type> ListTypes = new List<Type>
        {
            typeof (IEnumerable),
            typeof (ICollection),
            typeof (IList)
        };

        /// <summary>
        /// Тип System.String
        /// </summary>
        private static readonly Type StringType = typeof (string);

        /// <summary>
        /// Тип System.DateTime
        /// </summary>
        private static readonly Type DateTimeType = typeof(DateTime);

        /// <summary>
        /// Тип nullable System.DateTime
        /// </summary>
        private static readonly Type NullableDateTimeType = typeof(DateTime?);

        /// <summary>
        /// Тип System.Decimal
        /// </summary>
        private static readonly Type DecimalType = typeof(decimal);

        /// <summary>
        /// Возвращает значение, является ли аттрибут простым типом
        /// </summary>
        /// <param name="propertyInfo">Информация об аттрибуте</param>
        /// <returns>Значение, является ли аттрибут простым типом</returns>
        public static bool IsSimple(this PropertyInfo propertyInfo)
        {
            var type = propertyInfo.PropertyType;

            return type.IsPrimitive || type == StringType || type == DateTimeType || type == NullableDateTimeType || type == DecimalType;
        }

        /// <summary>
        /// Возвращает значение, является ли аттрибут типом, состоящим из списка простых типов
        /// </summary>
        /// <param name="propertyInfo">Информация об аттрибуте</param>
        /// <returns>Значение является ли аттрибут типом, состоящим из списка простых типов</returns>
        public static bool IsSimpleList(this PropertyInfo propertyInfo)
        {
            var isListType = ListTypes.Contains(propertyInfo.PropertyType);
            var isImplementsListType = propertyInfo.PropertyType.GetInterfaces().Any(i => ListTypes.Contains(i));
            var isStringType = propertyInfo.PropertyType == StringType;
            var isGenericArgumentSimple =
                propertyInfo.PropertyType.GenericTypeArguments.Any(t => t.IsPrimitive || t == StringType);

            return (isListType || isImplementsListType) && !isStringType && isGenericArgumentSimple;
        }

        /// <summary>
        /// Возвращает значение, является ли аттрибут типом, состоящим из списка объектов
        /// </summary>
        /// <param name="propertyInfo">Информация об аттрибуте</param>
        /// <returns>Значение является ли аттрибут типом, состоящим из списка объектов</returns>
        public static bool IsObjectList(this PropertyInfo propertyInfo)
        {
            var isListType = ListTypes.Contains(propertyInfo.PropertyType);
            var isImplementsListType = propertyInfo.PropertyType.GetInterfaces().Any(i => ListTypes.Contains(i));
            var isStringType = propertyInfo.PropertyType == StringType;
            var isGenericArgumentSimple =
                propertyInfo.PropertyType.GenericTypeArguments.Any(t => t.IsPrimitive || t == StringType);

            return (isListType || isImplementsListType) && !isStringType && !isGenericArgumentSimple;
        }
        
        /// <summary>
        /// Возвращает значение, является ли аттрибут сложным типом
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static bool IsCompoundObject(this PropertyInfo propertyInfo)
        {
            var isListType = ListTypes.Contains(propertyInfo.PropertyType);
            var isImplementsListType = propertyInfo.PropertyType.GetInterfaces().Any(i => ListTypes.Contains(i));
            var isStringType = propertyInfo.PropertyType == StringType;
            var isGenericArgumentSimple =
                propertyInfo.PropertyType.GenericTypeArguments.Any(t => t.IsPrimitive || t == StringType);

            return propertyInfo.PropertyType.IsClass && !isListType && !isImplementsListType && !isStringType && !isGenericArgumentSimple;
        }
    }
}