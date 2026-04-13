using System;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;

namespace Moedelo.Common.Kafka.Saga.Abstractions.Builders
{
    public static class MoedeloSagaBuilderExtensions
    {
        public static IMoedeloSagaBuilder DoneOnSuccessReply<TStateData, TNewStateData, TCommand>(
            this IMoedeloSagaBuilder builder,
            Func<TStateData, TNewStateData> mapState,
            Func<TNewStateData, Task> onBeforeNewStateSaveCallback = null)
            where TStateData : ISagaStateData
            where TNewStateData : ISagaStateData
            where TCommand : IEntityCommandData
        {
            return builder
                .OnSuccessReply<TStateData, TNewStateData, TCommand>(
                    mapState,
                    _ => Array.Empty<Func<ReplyTo, IMoedeloEntityCommandKafkaMessageDefinition<TCommand>>>(),
                    onBeforeNewStateSaveCallback
                );
        }

        public static IMoedeloSagaBuilder DoneOnErrorReply<TStateData, TNewStateData, TCommand>(
            this IMoedeloSagaBuilder builder,
            Func<TStateData, TNewStateData> mapState,
            Func<TNewStateData, Task> onBeforeNewStateSaveCallback = null)
            where TStateData : ISagaStateData
            where TNewStateData : ISagaStateData
            where TCommand : IEntityCommandData
        {
            return builder
                .OnErrorReply<TStateData, TNewStateData, TCommand>(
                    mapState,
                    _ => Array.Empty<Func<ReplyTo, IMoedeloEntityCommandKafkaMessageDefinition<TCommand>>>(),
                    onBeforeNewStateSaveCallback
                );
        }
    }
}