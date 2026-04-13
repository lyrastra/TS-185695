using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Moedelo.CommonV2.Utils.Enums
{
    /// <summary>
    /// Helper для работы с enum-ами
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// Получение атрибута указаного типа для конкретного значения перечисления
        /// </summary>
        /// <typeparam name="TAttribute">Тип искомого атрибута</typeparam>
        /// <typeparam name="TEnum">Тип перечисления (enum-а)</typeparam>
        /// <param name="enumItem">Анализируемое значение</param>
        public static TAttribute GetEnumItemAttribute<TAttribute, TEnum>(TEnum enumItem) where TAttribute : Attribute
        {
            var enumType = typeof(TEnum);
            var fieldName = Enum.GetName(enumType, enumItem);
            var field = enumType.GetField(fieldName);

            return (TAttribute)field.GetCustomAttribute(typeof(TAttribute));
        }

        /// <summary>
        /// Получения словаря "значение enum-а -> значение атрибута"
        /// </summary>
        /// <typeparam name="TEnum">Тип перечисления (enum-а)</typeparam>
        /// <typeparam name="TAttribute">Тип искомых атрибута</typeparam>
        public static IReadOnlyDictionary<TEnum, TAttribute> EnumToEnumAttributeDictionary<TEnum, TAttribute>() where TAttribute : Attribute
        {
            var enumAttributeDictionary = new Dictionary<TEnum, TAttribute>();
            var enumValues = Enum.GetValues(typeof(TEnum));

            foreach (TEnum enumItem in enumValues)
            {
                var descriptionValue = GetEnumItemAttribute<TAttribute, TEnum>(enumItem);
                enumAttributeDictionary.Add(enumItem, descriptionValue);
            }

            return enumAttributeDictionary;
        }

        /// <summary>
        /// Получение сопоставления "значение enum-а -> значение атрибута Description".
        /// Частный случай EnumToEnumAtributeDictionary для TAttribute is DescriptionAttribute
        /// </summary>
        /// <typeparam name="TEnum">Тип перечисления (enum-а)</typeparam>
        public static IReadOnlyDictionary<TEnum, string> EnumToDescriptionsDictionary<TEnum>()
        {
            var enumDescriptionsDictionary = EnumToEnumAttributeDictionary<TEnum, DescriptionAttribute>();

            return enumDescriptionsDictionary.ToDictionary(pair => pair.Key, pair => pair.Value?.Description);
        }
    }
}
