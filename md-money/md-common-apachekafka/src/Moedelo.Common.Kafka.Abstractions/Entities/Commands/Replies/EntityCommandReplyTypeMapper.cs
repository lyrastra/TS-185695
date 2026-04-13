using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies
{
    public static class EntityCommandReplyTypeMapper
    {
        private static readonly ConcurrentDictionary<Type, string> MapDictionary = new ConcurrentDictionary<Type, string>();

        public static string GetCommandReplyType<T>() where T : IEntityCommandReplyData
        {
            var type = typeof(T);
            RuntimeHelpers.RunClassConstructor(type.TypeHandle);

            return MapDictionary.TryGetValue(type, out var entityCommandReplyDataTypeString)
                ? entityCommandReplyDataTypeString
                : type.Name;
        }
        
        public static string GetCommandReplyType<T>(T replyData) where T : IEntityCommandReplyData
        {
            var type = replyData.GetType();
            return MapDictionary.TryGetValue(type, out var entityCommandReplyDataTypeString)
                ? entityCommandReplyDataTypeString
                : type.Name;
        }

        public static void AddTypeMap<T>(string entityCommandReplyDataTypeString) where T : IEntityCommandReplyData
        {
            var type = typeof(T);
            MapDictionary.AddOrUpdate(type, entityCommandReplyDataTypeString,
                (_1, _2) => entityCommandReplyDataTypeString);
        }
    }
}