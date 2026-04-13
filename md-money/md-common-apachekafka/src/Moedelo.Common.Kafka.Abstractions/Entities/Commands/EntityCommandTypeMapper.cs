using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace Moedelo.Common.Kafka.Abstractions.Entities.Commands
{
    public static class EntityCommandTypeMapper
    {
        private static readonly ConcurrentDictionary<Type, string> MapDictionary = new ConcurrentDictionary<Type, string>();
        
        public static string GetCommandType<T>() where T : IEntityCommandData
        {
            var type = typeof(T);
            RuntimeHelpers.RunClassConstructor(type.TypeHandle);

            return MapDictionary.TryGetValue(type, out var entityCommandDataTypeString)
                ? entityCommandDataTypeString
                : type.Name;
        }

        public static string GetCommandType<T>(T commandData) where T : IEntityCommandData
        {
            var type = commandData.GetType();

            return MapDictionary.TryGetValue(type, out var entityCommandDataTypeString)
                ? entityCommandDataTypeString
                : type.Name;
        }

        public static void AddTypeMap<T>(string entityCommandDataTypeString) where T : IEntityCommandData
        {
            var type = typeof(T);
            MapDictionary.AddOrUpdate(type, entityCommandDataTypeString,
                (_1, _2) => entityCommandDataTypeString);
        }
    }
}