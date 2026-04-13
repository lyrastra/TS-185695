using System;
using System.Collections.Generic;

namespace Moedelo.Changelog.Shared.Domain
{
    public class EntityFieldDefinition
    {
        /// <summary>
        /// Имя поля (как оно названо внутри типа)
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Имя для отображения
        /// </summary>
        public string ViewName { get; set; }
        /// <summary>
        /// Тип поля (может отсутствовать) см. <see cref="Moedelo.Changelog.Shared.Domain.Definitions.FieldTypes"/>
        /// </summary>
        public string FieldType { get; set; }
        /// <summary>
        /// Тип свойства
        /// </summary>
        public Type PropertyType { get; set; }
        /// <summary>
        /// Определение вложенных полей
        /// </summary>
        public IReadOnlyDictionary<string, EntityFieldDefinition> Fields { get; set; }
        /// <summary>
        /// Функция для форматирования строкового представления из сырого значения
        /// </summary>
        public Func<string, string> ValueFormatter { get; set; }
        /// <summary>
        /// Имя поля, которое хранит полный json-слепок всего состояния
        /// </summary>
        public string FullStatePropertyName { get; set; }
    }
}
