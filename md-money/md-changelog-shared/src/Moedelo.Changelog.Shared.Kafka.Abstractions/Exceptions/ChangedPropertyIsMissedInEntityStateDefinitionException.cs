using System;
using System.Collections.Generic;
using Moedelo.Changelog.Shared.Domain;

namespace Moedelo.Changelog.Shared.Kafka.Abstractions.Exceptions
{
    /// <summary>
    /// Исключение "Измененное свойство отсутствует в определении состояния для истории изменений"
    /// </summary>
    public sealed class ChangedPropertyIsMissedInEntityStateDefinitionException : Exception
    {
        public EntityStateDefinition StateDefinition { get; }

        public IReadOnlyCollection<string> FieldNames { get; }

        public ChangedPropertyIsMissedInEntityStateDefinitionException(
            EntityStateDefinition stateDef,
            IReadOnlyCollection<string> fieldNames)
            : base($"В определении {stateDef.GetType().Name} отсутствуют поля с именами ({string.Join(",", fieldNames)}). Отсылка таких изменений имеет мало смысла и может привести к порче данных истории изменений объекта")
        {
            StateDefinition = stateDef;
            FieldNames = fieldNames;
        }
    }
}
