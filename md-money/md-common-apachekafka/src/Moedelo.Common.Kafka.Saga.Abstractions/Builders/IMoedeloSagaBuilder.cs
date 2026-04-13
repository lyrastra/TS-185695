using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;

namespace Moedelo.Common.Kafka.Saga.Abstractions.Builders
{
    public interface IMoedeloSagaBuilder
    {
        IMoedeloSagaBuilder OnStart<TInitStateData, TCommand>(
            Func<TInitStateData, Func<ReplyTo, IMoedeloEntityCommandKafkaMessageDefinition<TCommand>>> createDescriptionHandler,
            Func<TInitStateData, Task> onBeforeInitStateSaveCallback = null)
            where TInitStateData : ISagaStateData
            where TCommand : IEntityCommandData;

        IMoedeloSagaBuilder OnStart<TInitStateData, TCommand>(
            Func<TInitStateData, IReadOnlyCollection<Func<ReplyTo, IMoedeloEntityCommandKafkaMessageDefinition<TCommand>>>> createDescriptionsHandlers,
            Func<TInitStateData, Task> onBeforeInitStateSaveCallback = null)
            where TInitStateData : ISagaStateData
            where TCommand : IEntityCommandData;

        IMoedeloSagaBuilder OnSuccessReply<TStateData, TReplyData, TNewStateData, TCommand>(
            Func<TStateData, TReplyData, TNewStateData> mapState,
            Func<TNewStateData, Func<ReplyTo, IMoedeloEntityCommandKafkaMessageDefinition<TCommand>>> createDescriptionHandler,
            Func<TNewStateData, Task> onBeforeNewStateSaveCallback = null)
            where TStateData : ISagaStateData
            where TNewStateData : ISagaStateData
            where TCommand : IEntityCommandData;

        IMoedeloSagaBuilder OnSuccessReply<TStateData, TReplyData, TNewStateData, TCommand>(
            Func<TStateData, TReplyData, TNewStateData> mapState,
            Func<TNewStateData, IReadOnlyCollection<Func<ReplyTo, IMoedeloEntityCommandKafkaMessageDefinition<TCommand>>>> createDescriptionsHandlers,
            Func<TNewStateData, Task> onBeforeNewStateSaveCallback = null)
            where TStateData : ISagaStateData
            where TNewStateData : ISagaStateData
            where TCommand : IEntityCommandData;

        IMoedeloSagaBuilder OnSuccessReply<TStateData, TNewStateData, TCommand>(
            Func<TStateData, TNewStateData> mapState,
            Func<TNewStateData, Func<ReplyTo, IMoedeloEntityCommandKafkaMessageDefinition<TCommand>>> createDescriptionHandler,
            Func<TNewStateData, Task> onBeforeNewStateSaveCallback = null)
            where TStateData : ISagaStateData
            where TNewStateData : ISagaStateData
            where TCommand : IEntityCommandData;

        IMoedeloSagaBuilder OnSuccessReply<TStateData, TNewStateData, TCommand>(
            Func<TStateData, TNewStateData> mapState,
            Func<TNewStateData, IReadOnlyCollection<Func<ReplyTo, IMoedeloEntityCommandKafkaMessageDefinition<TCommand>>>> createDescriptionsHandlers,
            Func<TNewStateData, Task> onBeforeNewStateSaveCallback = null)
            where TStateData : ISagaStateData
            where TNewStateData : ISagaStateData
            where TCommand : IEntityCommandData;

        IMoedeloSagaBuilder OnErrorReply<TStateData, TReplyData, TNewStateData, TCommand>(
            Func<TStateData, TReplyData, TNewStateData> mapState,
            Func<TNewStateData, Func<ReplyTo, IMoedeloEntityCommandKafkaMessageDefinition<TCommand>>> createDescriptionHandler,
            Func<TNewStateData, Task> onBeforeNewStateSaveCallback = null)
            where TStateData : ISagaStateData
            where TNewStateData : ISagaStateData
            where TCommand : IEntityCommandData;

        IMoedeloSagaBuilder OnErrorReply<TStateData, TReplyData, TNewStateData, TCommand>(
            Func<TStateData, TReplyData, TNewStateData> mapState,
            Func<TNewStateData, IReadOnlyCollection<Func<ReplyTo, IMoedeloEntityCommandKafkaMessageDefinition<TCommand>>>> createDescriptionsHandlers,
            Func<TNewStateData, Task> onBeforeNewStateSaveCallback = null)
            where TStateData : ISagaStateData
            where TNewStateData : ISagaStateData
            where TCommand : IEntityCommandData;

        IMoedeloSagaBuilder OnErrorReply<TStateData, TNewStateData, TCommand>(
            Func<TStateData, TNewStateData> mapState,
            Func<TNewStateData, Func<ReplyTo, IMoedeloEntityCommandKafkaMessageDefinition<TCommand>>> createDescriptionHandler,
            Func<TNewStateData, Task> onBeforeNewStateSaveCallback = null)
            where TStateData : ISagaStateData
            where TNewStateData : ISagaStateData
            where TCommand : IEntityCommandData;

        IMoedeloSagaBuilder OnErrorReply<TStateData, TNewStateData, TCommand>(
            Func<TStateData, TNewStateData> mapState,
            Func<TNewStateData, IReadOnlyCollection<Func<ReplyTo, IMoedeloEntityCommandKafkaMessageDefinition<TCommand>>>> createDescriptionsHandlers,
            Func<TNewStateData, Task> onBeforeNewStateSaveCallback = null)
            where TStateData : ISagaStateData
            where TNewStateData : ISagaStateData
            where TCommand : IEntityCommandData;

        IMoedeloSagaBuilder OnUnexpectedReply(UnexpectedSagaReplyReaction reaction);

        IMoedeloSagaBuilder OnUnexpectedReply(
            Func<UnexpectedSagaReplyDescription, Task<UnexpectedSagaReplyReaction>> handler);
    }
}