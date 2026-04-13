using System;
using System.Collections.Generic;

namespace Moedelo.Changelog.Shared.Kafka.Abstractions.Exceptions
{
    /// <summary>
    /// Исключение "Изменённое свойство отсутствует в типе изменённого объекта"
    /// </summary>
    public sealed class ChangedPropertyIsMissedInEntityTypeException : Exception
    {
        public IReadOnlyCollection<string> FieldNames { get; }

        public ChangedPropertyIsMissedInEntityTypeException(Type type, string fieldName)
          : this(type, new []{ fieldName})
        {
        }
        
        public ChangedPropertyIsMissedInEntityTypeException(Type type, IReadOnlyCollection<string> fieldNames)
        : base ($"Не удалось найти поля с именами ({string.Join(",", fieldNames)}) в типе {type.Name}")
        {
            FieldNames = fieldNames;
        }
    }
}
