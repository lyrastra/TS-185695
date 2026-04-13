using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Moedelo.Changelog.Shared.Domain;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Exceptions;
using Moedelo.Infrastructure.Json;

namespace Moedelo.Changelog.Shared.Kafka.Abstractions.Extensions
{
    public static class ExplicitChangesMapExtensions
    {
        /// <summary>
        /// Кэш свойств 
        /// </summary>
        private static readonly ConcurrentDictionary<Type, IReadOnlyDictionary<string, PropertyInfo>>
            TypePropertiesCache = new ConcurrentDictionary<Type, IReadOnlyDictionary<string, PropertyInfo>>();
        
        /// <summary>
        /// Создать список явно заданных изменений на основе изменяемого объекта
        /// </summary>
        /// <param name="entity">изменяемый объект</param>
        /// <param name="changedFields">список полей (названия), которые были изменены</param>
        /// <param name="stateDef">определение состояния, используемое для хранения информации об объектах этого типа в истории изменений</param>
        /// <typeparam name="TEntityType"></typeparam>
        /// <returns></returns>
        public static IReadOnlyCollection<ExplicitChangesSaveCommandFields.FieldChange> ToExplicitFieldChanges<TEntityType>(
            this TEntityType entity,
            string[] changedFields,
            EntityStateDefinition stateDef) where TEntityType: class
        {
            var type = typeof(TEntityType);

            var typeProperties = TypePropertiesCache
                .GetOrAdd(type, CreateTypePropertiesMap);
            
            EnsureTypeContainsAllChangedProps(changedFields, typeProperties, type);
            EnsureStateDefinitionContainsAllChangedProps(changedFields, stateDef);

            return changedFields
                .Select(prop =>
                {
                    var typeProp = typeProperties[prop];
                    var fieldDef = stateDef.Fields[prop];
            
                    return new ExplicitChangesSaveCommandFields.FieldChange
                    {
                        FieldName = prop,
                        NewValue = GetJsonValue(typeProp.GetValue(entity), fieldDef)
                    };
                })
                .ToArray();
        }

        /// <summary>
        /// Создать список явно заданных изменений
        /// </summary>
        /// <param name="changes">список изменений (название + новое значение)</param>
        /// <param name="stateDef">определение состояния, используемое для хранения информации об объектах этого типа в истории изменений</param>
        /// <returns></returns>
        public static IReadOnlyCollection<ExplicitChangesSaveCommandFields.FieldChange> ToExplicitFieldChanges(
            this (string FieldName, object NewValue)[] changes,
            EntityStateDefinition stateDef)
        {
            var changedFields = changes.Select(x => x.FieldName);
            EnsureStateDefinitionContainsAllChangedProps(changedFields, stateDef);

            return changes
                .Select(propChange =>
                {
                    var fieldDef = stateDef.Fields[propChange.FieldName];
                    return new ExplicitChangesSaveCommandFields.FieldChange
                    {
                        FieldName = propChange.FieldName,
                        NewValue = GetJsonValue(propChange.NewValue, fieldDef)
                    };
                })
                .ToArray();
        }

        private static string GetJsonValue(object value, EntityFieldDefinition stateDefField)
        {
            return stateDefField.PropertyType == typeof(string) 
                ? value?.ToString() 
                : value.ToJsonString();
        }

        private static void EnsureStateDefinitionContainsAllChangedProps(
            IEnumerable<string> changedFields,
            EntityStateDefinition stateDef)
        {
            var missedProps = changedFields
                .Where(prop => !stateDef.Fields.ContainsKey(prop))
                .ToArray();

            if (missedProps.Any())
            {
                throw new ChangedPropertyIsMissedInEntityStateDefinitionException(
                    stateDef, missedProps.Select(prop => prop).ToArray());
            }
        }

        private static void EnsureTypeContainsAllChangedProps(
            IEnumerable<string> changedFields,
            IReadOnlyDictionary<string, PropertyInfo> typeProperties, Type type)
        {
            var missedProps = changedFields
                .Where(prop => !typeProperties.ContainsKey(prop))
                .ToArray();

            if (missedProps.Any())
            {
                throw new ChangedPropertyIsMissedInEntityTypeException(
                    type, missedProps.Select(prop => prop).ToArray());
            }
        }

        private static IReadOnlyDictionary<string, PropertyInfo> CreateTypePropertiesMap(Type type)
        {
            return type
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(prop => prop.Name);
        }
    }
}
