using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace Moedelo.Common.Kafka.Abstractions.Entities.Events
{
    public static class EntityEventTypeMapper
    {
        private static readonly ConcurrentDictionary<Type, string> MapDictionary = new ConcurrentDictionary<Type, string>();
        
        public static string GetEventType<T>() where T : IEntityEventData
        {
            var type = typeof(T);
            RuntimeHelpers.RunClassConstructor(type.TypeHandle);

            return MapDictionary.TryGetValue(type, out var entityEventDataTypeString)
                ? entityEventDataTypeString
                : type.Name;
        }

        public static string GetEventType<T>(T eventData) where T : IEntityEventData
        {
            var type = eventData.GetType();

            return MapDictionary.TryGetValue(type, out var entityEventDataTypeString)
                ? entityEventDataTypeString
                : type.Name;
        }

        public static void AddTypeMap<T>(string entityEventDataTypeString) where T : IEntityEventData
        {
            var type = typeof(T);
            MapDictionary.AddOrUpdate(type, entityEventDataTypeString,
                (_1, _2) => entityEventDataTypeString);
        }
    }
}