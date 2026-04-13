using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace Moedelo.Common.Kafka.Saga.Abstractions
{
    internal static class SagaStateTypeMapper
    {
        private static readonly ConcurrentDictionary<Type, string> MapDictionary = new ConcurrentDictionary<Type, string>();
        
        public static string GetStateType<T>() where T : ISagaStateData
        {
            var type = typeof(T);
            RuntimeHelpers.RunClassConstructor(type.TypeHandle);

            return MapDictionary.TryGetValue(type, out var stateDataTypeString)
                ? stateDataTypeString
                : type.Name;
        }

        public static string GetStateType<T>(T stateData) where T : ISagaStateData
        {
            var type = stateData.GetType();

            return MapDictionary.TryGetValue(type, out var stateDataTypeString)
                ? stateDataTypeString
                : type.Name;
        }

        public static void AddTypeMap<T>(string stateDataTypeString) where T : ISagaStateData
        {
            var type = typeof(T);
            MapDictionary.AddOrUpdate(type, stateDataTypeString,
                (_1, _2) => stateDataTypeString);
        }
    }
}